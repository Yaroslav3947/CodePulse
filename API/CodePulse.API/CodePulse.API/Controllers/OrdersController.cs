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
            this._orderRepository = orderRepository;
            this._productRepository = productRepository;
        }

        // POST: {apibaseurl}/api/orders
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto request)
        {

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                TotalAmount = request.TotalAmount,
                Status = request.Status,
                UserId = request.UserId,
                Products = new List<Product>()
            };

            // Add products to the order
            foreach (var productId in request.Products)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product != null)
                {
                    order.Products.Add(product);
                }
            }

            // Save order to the repository
            order = await _orderRepository.CreateAsync(order);

            // Convert Domain model to Dto
            var response = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                UserId = order.UserId,
                Products = order.Products.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToList()
            };

            return Ok(response);
        }

        // GET: {apibaseurl}/api/orders
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            var response = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                UserId = order.UserId,
                Products = order.Products.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToList()
            }).ToList();

            return Ok(response);
        }

        // GET: {apiBaseUrl}/api/orders/{id}
        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "User,Admin")]
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
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                UserId = order.UserId,
                Products = order.Products.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToList()
            };

            return Ok(response);
        }

        // PUT: {apiBaseUrl}/api/orders/{id}
        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderRequestDto request)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            // Update order details
            order.Status = request.Status;
            order.TotalAmount = request.TotalAmount;

            // Update order in the repository
            order = await _orderRepository.UpdateAsync(order);

            // Convert Domain model to Dto
            var response = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                UserId = order.UserId,
                Products = order.Products.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToList()
            };

            return Ok(response);
        }

        // DELETE: {apiBaseUrl}/api/orders/{id}
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
        {
            var order = await _orderRepository.DeleteAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
