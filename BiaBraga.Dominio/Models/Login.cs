using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Domain.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Você deve informar o seu login de acesso")]
        [Display(Name = "Login")]
        public string LoginUser { get; set; }
        [Required(ErrorMessage = "Você deve informar sua senha de acesso!")]
        [Display(Name = "Senha")]
        public string Password { get; set; } 
    }
}
