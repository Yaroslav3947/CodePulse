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

            public ICollection<Product> Products { get; set; } 

        }
    }

}
