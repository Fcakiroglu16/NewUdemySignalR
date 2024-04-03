using System.ComponentModel.DataAnnotations;

namespace UdemySampleProject.Web.Models.ViewModels
{
    public record SignInViewModel([Required] string Email,[Required] string Password);
}
