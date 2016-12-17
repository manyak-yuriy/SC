using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(256, ErrorMessage = "Имя не должно быть больше чем 256 символов.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256, ErrorMessage = "Эмейл не должен быть больше чем 256 символов.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}