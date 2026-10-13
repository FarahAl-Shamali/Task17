using ProductCatalogSystem.Models;

namespace ProductCatalogSystem.ViewModel
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Rating { get; set; }
        public string Description { get; set; }
     
    }
}
