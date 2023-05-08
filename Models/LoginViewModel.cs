using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace tuto.Models
{
    public class LoginViewModel
    {
        public int Id { get; set; } 

        [System.ComponentModel.DataAnnotations.Required]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
