using CodePulse.API.Models.Domain;

namespace CodePulse.API.Models.DTO {
    public class CreateProductRequestDto {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public Guid[] Categories { get; set; }
    }
}
