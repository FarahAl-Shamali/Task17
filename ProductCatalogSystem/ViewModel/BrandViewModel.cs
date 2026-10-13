using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class BrandViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
