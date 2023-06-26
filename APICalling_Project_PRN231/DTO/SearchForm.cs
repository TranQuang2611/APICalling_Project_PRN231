namespace APICalling_Project_PRN231.DTO
{
    public class SearchForm
    {
        
        public List<int?> catId { get; set; } = new List<int?>();
        public List<int?> colorId { get; set; } = new List<int?>();
        public List<int?> brandId { get; set; } = new List<int?>();
        public List<int?> sizeId { get; set; } = new List<int?>();
        public List<int?> ramId { get; set; } = new List<int?>();
        public string minPrice { get; set; }   = string.Empty;
        public string maxPrice { get; set; } = string.Empty;
        public string nameProd { get; set; } = string.Empty;
    }
}
