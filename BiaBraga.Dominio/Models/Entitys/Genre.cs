using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Domain.Models.Entitys
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Gênero deve ter um nome")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Gênero deve ter um tamanho de 4 a 50 caracteres")]
        public string Name { get; set; }
    }
}
