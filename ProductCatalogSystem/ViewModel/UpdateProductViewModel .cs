using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? NewImage { get; set; }     
        public string? CurrentImage { get; set; }
    }
}
