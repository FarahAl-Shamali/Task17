using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.Models
{
    public class Product: Base
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Bradn { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
