using Microsoft.AspNetCore.Identity;
using ProductCatalogSystem.Enums;

namespace ProductCatalogSystem.Models
{
    public class User: IdentityUser
    {
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserType UserType { get; set; }
    }
}
