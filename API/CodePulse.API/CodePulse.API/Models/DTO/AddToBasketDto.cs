namespace CodePulse.API.Models.DTO {
    public class AddToBasketDto {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
