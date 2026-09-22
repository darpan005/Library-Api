using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs
{
    public class CreateBookDto
    {
        [Required]
        public string Title{get; set;}=string.Empty;

        [Required]
        public string Author{get; set;}=string.Empty;

        [Range(1,1000)]
        public int TotalCopies{get; set;}
    }


    public class UpdateBookDto
    {
        [Required]
        public string Title{get; set;}=string.Empty;

        [Required]
        public string Author{get; set;}=string.Empty;

        [Range(1,1000)]
        public int TotalCopies{get; set;}

        [Range(0,1000)]
        public int AvailableCopies{get; set;}
    }
}