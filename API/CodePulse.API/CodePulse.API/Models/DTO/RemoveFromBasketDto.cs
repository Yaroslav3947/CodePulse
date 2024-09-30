namespace CodePulse.API.Models.DTO {
    public class RemoveFromBasketDto {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
