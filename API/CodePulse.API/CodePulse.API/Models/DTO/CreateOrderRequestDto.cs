using CodePulse.API.Models.Domain;

namespace CodePulse.API.Models.DTO {
    public class CreateOrderRequestDto {

            public DateTime OrderDate { get; set; }

            public double TotalAmount { get; set; }

            public string Status { get; set; }

            public Guid UserId { get; set; }

            public Guid[] Products { get; set; }
    }
}
