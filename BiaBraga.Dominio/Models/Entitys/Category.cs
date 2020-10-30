using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Domain.Models.Entitys
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Nome deve ter um tamanho de 5 a 20 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "Descrição deve ter no máximo 200 caracteres")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}