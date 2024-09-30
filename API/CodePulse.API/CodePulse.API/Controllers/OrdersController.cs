using CodePulse.API.Models.Domain;
using CodePulse.API.Models.Domain.CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrdersController(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        // POST: {apibaseurl}/api/orders
        [HttpPost]
        //[Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto request)
        {
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                Status = request.Status,
                UserId = request.UserId,
                OrderItems = new List<OrderItem>() // Initialize order items
            };

            // Add products to the order
            foreach (var productId in request.Products)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product != null)
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = productId,
                        Quantity = 1, // Assuming a default quantity of 1 for simplicity
                        Price = product.Price // Store the price at order creation
                    });
                }
            }

            // Save order to the repository
            order = await _orderRepository.CreateAsync(order);

            // Convert Domain model to Dto
            var response = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(x => new OrderItemDto
                {
                    ProductId = x.ProductId,
                    Product = x.Product, // Directly set the Product from OrderItem
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };

            return Ok(response);
        }

        // GET: {apiBaseUrl}/api/orders/{id}
        [HttpGet("{id:Guid}")]
        //[Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            // Convert to DTO
            var response = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(x => new OrderItemDto
                {
                    ProductId = x.ProductId,
                    Product = x.Product, // Get full product details
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };

            return Ok(response);
        }

        // GET: {apibaseurl}/api/orders
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            var response = await Task.WhenAll(orders.Select(async order =>
            {
                var orderWithItems = await _orderRepository.GetByIdAsync(order.Id);
                return new OrderDto
                {
                    Id = orderWithItems.Id,
                    OrderDate = orderWithItems.OrderDate,
                    Status = orderWithItems.Status,
                    UserId = orderWithItems.UserId,
                    OrderItems = orderWithItems.OrderItems.Select(x => new OrderItemDto
                    {
                        ProductId = x.ProductId,
                        Product = x.Product, // Get product details
                        Quantity = x.Quantity,
                        Price = x.Price
                    }).ToList()
                };
            }));

            return Ok(response);
        }

        // PUT: {apiBaseUrl}/api/orders/{id}
        [HttpPut("{id:Guid}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderRequestDto request)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            // Update order details
            order.Status = request.Status;

            // Update order in the repository
            order = await _orderRepository.UpdateAsync(order);

            // Convert Domain model to Dto
            var response = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(x => new OrderItemDto
                {
                    ProductId = x.ProductId,
                    Product = x.Product, // Get product details
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };

            return Ok(response);
        }

        // DELETE: {apiBaseUrl}/api/orders/{id}
        [HttpDelete("{id:Guid}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            var order = await _orderRepository.DeleteAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Add Product to Order: {apiBaseUrl}/api/orders/{orderId}/add-product/{productId}
        [HttpPost("{orderId:Guid}/add-product/{productId:Guid}")]
        //[Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> AddProductToOrderAsync([FromRoute] Guid orderId, [FromRoute] Guid productId, [FromQuery] int quantity) {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if(order == null) {
                return NotFound();
            }

            var product = await _productRepository.GetByIdAsync(productId);
            if(product == null) {
                return BadRequest("Product not found.");
            }

            var existingOrderItem = order.OrderItems.FirstOrDefault(item => item.ProductId == productId);
            if(existingOrderItem != null) {
                existingOrderItem.Quantity += quantity;
            } else {
                order.OrderItems.Add(new OrderItem {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                });
            }

            await _orderRepository.UpdateAsync(order);

            var response = CreateOrderDto(order);
            return Ok(response);
        }

        // Update Product Quantity in Order: {apiBaseUrl}/api/orders/{orderId}/update-product-quantity/{productId}
        [HttpPut("{orderId:Guid}/update-product-quantity/{productId:Guid}")]
        //[Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateProductQuantityInOrderAsync([FromRoute] Guid orderId, [FromRoute] Guid productId, [FromQuery] int newQuantity) {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if(order == null) {
                return NotFound();
            }

            var orderItem = order.OrderItems.FirstOrDefault(item => item.ProductId == productId);
            if(orderItem == null) {
                return BadRequest("Product not found in order.");
            }

            orderItem.Quantity = newQuantity;
            await _orderRepository.UpdateAsync(order);

            var response = CreateOrderDto(order);
            return Ok(response);
        }

        // Remove Product from Order: {apiBaseUrl}/api/orders/{orderId}/remove-product/{productId}
        [HttpDelete("{orderId:Guid}/remove-product/{productId:Guid}")]
        //[Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> RemoveProductFromOrderAsync([FromRoute] Guid orderId, [FromRoute] Guid productId) {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if(order == null) {
                return NotFound();
            }

            var orderItem = order.OrderItems.FirstOrDefault(item => item.ProductId == productId);
            if(orderItem == null) {
                return BadRequest("Product not found in order.");
            }

            order.OrderItems.Remove(orderItem);
            await _orderRepository.UpdateAsync(order);

            var response = CreateOrderDto(order);
            return Ok(response);
        }

        private OrderDto CreateOrderDto(Order order) {
            return new OrderDto {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(x => new OrderItemDto {
                    ProductId = x.ProductId,
                    Product = x.Product, // Get product details
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };
        }

        // GET: {apiBaseUrl}/api/orders/user/{userId}
        [HttpGet("user/{userId:Guid}")]
        //[Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetOrderByUserId([FromRoute] Guid userId) {
            var order = await _orderRepository.GetByUserIdAsync(userId);

            if(order == null) {
                return NotFound();
            }

            // Convert to DTO
            var response = new OrderDto {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(x => new OrderItemDto {
                    ProductId = x.ProductId,
                    Product = x.Product,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };

            return Ok(response); 
        }
    }
}
