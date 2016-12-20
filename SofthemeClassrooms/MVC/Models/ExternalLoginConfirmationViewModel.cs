using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [MaxLength(256, ErrorMessage = "Эмейл не должен быть больше чем 256 символов.")]
        public string Email { get; set; }
    }
}