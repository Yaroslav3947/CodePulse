namespace CodePulse.API.Models.Domain {
    public class CosmeticLike {
        public Guid Id { get; set; }
        public Guid CosmeticId { get; set; }
        public Guid UserId { get; set; }
    }
}
