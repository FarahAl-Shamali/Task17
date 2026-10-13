namespace ProductCatalogSystem.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public bool IsDelete { get; set; }
        public List<Product> Products { get; set; }
    }
}
