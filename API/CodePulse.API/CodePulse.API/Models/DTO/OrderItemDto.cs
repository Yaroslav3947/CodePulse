using CodePulse.API.Models.Domain;
using System;

namespace CodePulse.API.Models.DTO
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalCost => Price * Quantity;
    }
}