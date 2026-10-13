using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class CreateBrandViewModel
    {
        [Required]
        public String Name { get; set; }
    }
}
