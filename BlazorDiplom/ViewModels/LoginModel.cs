using System.ComponentModel.DataAnnotations;

namespace BlazorDiplom.ViewModels
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Введен некорректный e-mail адрес")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 20 символов")]
        public string Password { get; set; } = string.Empty;
    }
}
