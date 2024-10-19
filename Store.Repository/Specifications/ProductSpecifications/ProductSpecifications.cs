namespace Store.Repository.Specifications.ProductSpecifications
{
    public class ProductSpecifications
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }
        public int PageIndex { get; set; } = 1;

        public const int MAXPAGESIZE = 50;

        private int _PageSize = 6;
        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MAXPAGESIZE) ? int.MaxValue : value;
        }

        private string? _search;

        public string? Search
        {
            get => Search;
            set => _search = value?.Trim().ToLower();
        }

    }
}
