namespace ProductCatalogSystem.Models
{
    public class Review
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Rating { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
