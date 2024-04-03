using System.ComponentModel.DataAnnotations;

namespace UdemySampleProject.Web.Models.ViewModels
{
    public record SignUpViewModel([Required]string Email, [Required] string Password, [Required] string ConfirmPassword);
}
