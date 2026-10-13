using ProductCatalogSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class UpdateReviewViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]

        public string Description { get; set; }
    

        public int ProductId { get; set; }


        public Product? Product { get; set; }
    }
}
