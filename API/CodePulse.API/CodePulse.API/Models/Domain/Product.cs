namespace CodePulse.API.Models.Domain {
    public class Product {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
