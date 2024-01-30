namespace CodePulse.API.Models.DTO {
    public class AddLikeRequestDto {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
