using Org.BouncyCastle.Asn1.Crmf;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
