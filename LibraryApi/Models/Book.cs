namespace LibraryApi.Models
{
    public class Book
    {
        public string Id{get; set;}
        public string Title{get; set;}=string.Empty;
        public string Author{get; set;}=string.Empty;
        public int TotalCopies{get; set;}
        public int AvailableCopies{get; set;}
    }
}