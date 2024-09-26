namespace CodePulse.API.Models.Domain
{
    namespace CodePulse.API.Models.Domain
    {
        public class Order
        {
            public Guid Id { get; set; }  

            public DateTime OrderDate { get; set; } 

            public double TotalAmount { get; set; } 

            public string Status { get; set; } 

            public Guid UserId { get; set; }  
            public double TotalCost {  get; set; }

            public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        }
    }

}
