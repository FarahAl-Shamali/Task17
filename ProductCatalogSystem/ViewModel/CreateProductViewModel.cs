using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class CreateProductViewModel
    {
     
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
