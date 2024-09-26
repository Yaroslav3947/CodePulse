using CodePulse.API.Models.Domain;
using System;
using System.Collections.Generic;

namespace CodePulse.API.Models.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public double TotalCost => OrderItems.Sum(item => item.TotalCost);
    }
}
