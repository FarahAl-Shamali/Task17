using Forms.Web.Services;
using System.ComponentModel.DataAnnotations;

namespace Task17.Models
{
    public class product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public float price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
