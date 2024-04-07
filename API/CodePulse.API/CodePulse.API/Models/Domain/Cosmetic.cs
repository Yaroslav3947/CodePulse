namespace CodePulse.API.Models.Domain {
    public class Cosmetic {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
