using CodePulse.API.Models.Domain;

namespace CodePulse.API.Models.DTO {
    public class CosmeticDto {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    }
}
