using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class UpdateCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
