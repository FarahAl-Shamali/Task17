using ProductCatalogSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class CreateReviewViewModel
    {
        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Required]
       
        [Display(Name = "Review")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }
    }
}
