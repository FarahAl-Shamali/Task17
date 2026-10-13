using ProductCatalogSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalogSystem.ViewModel
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "User Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "User Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "User Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

    }
}