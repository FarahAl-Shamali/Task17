namespace ProductCatalogSystem.ViewModel
{
    public class PagingResultsViewModel<T>
    {
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Data { get; set; } = new();
    }
}
