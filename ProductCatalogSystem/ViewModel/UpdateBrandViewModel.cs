using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class UpdateBrandViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
