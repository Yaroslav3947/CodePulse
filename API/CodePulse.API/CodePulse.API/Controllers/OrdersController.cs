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
    public class OrdersController : ControllerBase {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrdersController(IOrderRepository orderRepository, IProductRepository productRepository) {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        //[Authorize(Roles = "Writer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto request) {
            var order = new Order {
                OrderDate = DateTime.UtcNow,
                Status = "Created",
                UserId = request.UserId,
                OrderItems = new List<OrderItem>()
            };

            order = await _orderRepository.CreateAsync(order);

            var response = new OrderDto {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                UserId = order.UserId,
                OrderItems = new List<OrderItemDto>(),
                TotalAmount = order.OrderItems.Sum(x => x.Price * x.Quantity)
            };

            return Ok(response);
        }


        // GET: {apiBaseUrl}/api/orders/{id}
        [HttpGet("{id:Guid}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid id) {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null) {
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
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList(),
                TotalAmount = order.OrderItems.Sum(x => x.Price * x.Quantity)
            };

            return Ok(response);
        }

        // GET: {apibaseurl}/api/orders
        [HttpGet]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetAllOrders() {
            var orders = await _orderRepository.GetAllAsync();

            var response = orders.Select(order => new OrderDto {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(x => new OrderItemDto {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList(),
                TotalAmount = order.OrderItems.Sum(x => x.Price * x.Quantity)
            }).ToList();

            return Ok(response);
        }


        // PUT: {apiBaseUrl}/api/orders/{id}
        [HttpPut("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderRequestDto request) {
            var updatedOrder = await _orderRepository.UpdateAsync(id, request.Status);

            if(updatedOrder == null) {
                return NotFound();
            }

            var response = new OrderDto {
                Id = updatedOrder.Id,
                OrderDate = updatedOrder.OrderDate,
                Status = updatedOrder.Status,
                UserId = updatedOrder.UserId,
                OrderItems = updatedOrder.OrderItems.Select(x => new OrderItemDto {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };

            return Ok(response);
        }


        // DELETE: {apiBaseUrl}/api/orders/{id}
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id) {
            var order = await _orderRepository.DeleteAsync(id);

            if (order == null) {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{orderId:Guid}/add-product/{productId:Guid}")]
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
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };
        }

        [HttpPut("{orderId:Guid}/update-product-quantity/{productId:Guid}")]
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

            var response = CreateOrderDto(order);
            return Ok(response);
        }

        [HttpDelete("{orderId:Guid}/remove-product/{productId:Guid}")]
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

            var response = CreateOrderDto(order);
            return Ok(response);
        }


        [HttpGet("user/{userId:Guid}")]
        public async Task<IActionResult> GetOrderByUserId([FromRoute] Guid userId) {
            var order = await _orderRepository.GetByUserIdAsync(userId);

            if(order == null) {
                return NotFound();
            }

            var response = new OrderDto {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(x => new OrderItemDto {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };

            return Ok(response); 
        }
    }
}
