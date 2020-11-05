using System;
using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Domain.Models.Entitys
{
    public class Contact
    {
        public int Id { get; set; }
        [Display(Name = "Nome completo")]
        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Nome deve ter um tamanho de 5 a 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mensagem é obrigatório")]
        [Display(Name = "Mensagem")]
        [StringLength(1000, MinimumLength = 50, ErrorMessage = "Mensagem deve ter um tamanho de 50 a 20000 caracteres")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        //True para novos contatos, false quando o usuario ler
        [Required]
        public bool New { get; set; }

        //Irá ter uma seção de contatos importantes, onde o usuario poderá deixar alguns em destaques temporariamente
        [Required]
        public bool Important { get; set; }

        [Required]
        [Display(Name = "Data do contato")]
        public DateTime Date { get; set; }
    }
}
