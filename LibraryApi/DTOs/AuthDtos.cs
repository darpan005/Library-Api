using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs
{
    public class RegisterDto
    {
        [Required,EmailAddress]
        public string Email{get; set;}=string.Empty;

        [Required, MinLength(6)]
        public string Password{get; set;}=string.Empty;

        public string Role{get; set;}="Member";
    }


    public class LoginDto
    {
        [Required,EmailAddress]
        public string Email{get; set;}=string.Empty;

        [Required]
        public string Password{get; set;}=string.Empty;
    }
}