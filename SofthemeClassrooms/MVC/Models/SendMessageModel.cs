using System.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SendMessageModel
    {
        [Required(ErrorMessage = "Это обязательное поле для заполнения.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это обязательное поле для заполнения.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Это обязательное поле для заполнения.")]
        [EmailAddress(ErrorMessage ="Введите правильный email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Это обязательное поле для заполнения.")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}