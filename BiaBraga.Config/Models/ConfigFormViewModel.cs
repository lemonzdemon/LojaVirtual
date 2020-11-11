using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Config.Models
{
    public class ConfigFormViewModel
    {
        [Display(Name = "String de conexão")]
        [Required(ErrorMessage = "String de conexão é obrigatório")]
        public string ConnectionString { get; set; }

        [Display(Name = "Chave secreta")]
        [Required(ErrorMessage = "Chave secreta é obrigatório")]
        public string SecretKey { get; set; }
    }
}
