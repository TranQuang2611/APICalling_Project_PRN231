namespace APICalling_Project_PRN231.DTO
{
    public class SearchForm
    {
        
        public List<int?> catId { get; set; } = new List<int?>();
        public List<int?> colorSearch { get; set; } = new List<int?>();
        //public List<int> sizeSearch { get; set; }
        public int sizeSearch { get; set; } = 0;
        public int ramSearch { get; set; } = 0;
        public string minPrice { get; set; }   = string.Empty;
        public string maxPrice { get; set; } = string.Empty;
    }
}
