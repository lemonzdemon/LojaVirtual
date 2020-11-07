using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Domain.Models.Entitys
{
    public class Department
    {
        public int Id { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(15, ErrorMessage = "Nome pode ter no máximo 15 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(200, ErrorMessage = "Descrição pode ter no máximo 200 caracteres")]
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
    }
}
