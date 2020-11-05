using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Domain.Models.Entitys
{
    /// <summary>
    /// Momento do cadastro irá exibir todos que estiver com prop Public igual a true
    /// e com uma opçao de personalizar, para que o usuario informe manualmente qual o seu genero
    /// nesse caso, sera setado prop public como falso
    /// </summary>
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Gênero deve ter um nome")]
        [Display(Name = "Gênero")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Gênero deve ter um tamanho de 4 a 50 caracteres")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Visível a todos os usuários")]
        public bool Public { get; set; }
    }
}
