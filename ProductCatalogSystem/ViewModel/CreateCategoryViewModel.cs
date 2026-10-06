using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
