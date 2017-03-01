using System.Web;
using System;
using System.ComponentModel.DataAnnotations;
using ManagementServices.Models;

namespace WebApplication1.Models
{
    public class SendMessageModel
    {
        [MaxLength(100, ErrorMessage ="Имя не должно быть больше 100 символов.")]
        [Required(ErrorMessage = "Это обязательное поле для заполнения.")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Фамилия не должно быть больше 100 символов.")]
        [Required(ErrorMessage = "Это обязательное поле для заполнения.")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "Эмейл не должно быть больше 100 символов.")]
        [Required(ErrorMessage = "Это обязательное поле для заполнения.")]
        [EmailAddress(ErrorMessage ="Введите правильный email.")]
        public string Email { get; set; }

        [MaxLength(1000, ErrorMessage = "Сообщение не должно быть больше 1000 символов.")]
        [Required(ErrorMessage = "Это обязательное поле для заполнения.")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public static FeedBackDTO ToFeedbackDTO(SendMessageModel model)
        {
            return new FeedBackDTO
            {
                Email = model.Email,
                LastName = model.LastName,
                Message = model.Message,
                Name = model.Name
            };
        }
    }
}