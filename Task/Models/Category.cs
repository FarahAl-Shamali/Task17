using System.ComponentModel.DataAnnotations;

namespace Task17.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<product> Products { get; set; }
    }
}
