using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.Models
{
    public class Category:Base
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
