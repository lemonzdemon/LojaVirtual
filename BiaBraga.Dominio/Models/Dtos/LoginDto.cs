using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Domain.Models.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Você deve informar o seu login de acesso")]
        [Display(Name = "Login")]
        public string LoginUser { get; set; }
        [Required(ErrorMessage = "Você deve informar sua senha de acesso!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; } 
    }
}
