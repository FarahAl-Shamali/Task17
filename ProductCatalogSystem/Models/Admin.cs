namespace ProductCatalogSystem.Models
{
    public class Admin:Base
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
