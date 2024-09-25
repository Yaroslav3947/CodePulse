using CodePulse.API.Models.Domain;

namespace CodePulse.API.Models.DTO {
    public class UpdateOrderRequestDto {
            public DateTime OrderDate { get; set; }

            public double TotalAmount { get; set; }

            public string Status { get; set; }

            public Guid UserId { get; set; }

            public List<Guid> Products { get; set; }
    }
}
