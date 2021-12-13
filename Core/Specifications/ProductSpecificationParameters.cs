namespace Core.Specifications
{
    public class ProductSpecificationParameters
    {
        private const int MAXPAGESIZE = 18;
        public int PageIndex { get; set; } = 1;

        private int _pageSize { get; set; } = 12;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAXPAGESIZE ? MAXPAGESIZE : value);
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }

        private string _search { get; set; }
        public string Search 
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
