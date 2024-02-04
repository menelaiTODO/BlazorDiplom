using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazorDiplom.ViewModels
{
    public class SignUpModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Введен некорректный e-mail адрес")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 20 символов")]
        public string FirstPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 20 символов")]
        public string SecondPassword { get; set; } = string.Empty;

        public List<string> Messages { get; set; } = [];
    }
}
