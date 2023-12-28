using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Modles.Dtos.Requests
{
    public class UserRegistartionDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
