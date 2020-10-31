using System;
using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Domain.Models.Entitys
{
    public class Product
    {
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Nome deve ter um tamanho de 5 a 100 caracteres")]
        public string Name { get; set; }


        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição obrigatória")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Descrição deve ter um tamanho de 10 a 500 caracteres")]
        public string Description { get; set; }

        [Display(Name = "Preço Atual")]
        [Required(ErrorMessage = "Preço atual obrigatório")]
        [Range(0, 9999.99, ErrorMessage = "Preço atual deve ser entre R$0.00 a R$9,999.99")]
        public decimal Price { get; set; }

        [Display(Name = "Preço Antigo")]
        [Range(0, 9999.99, ErrorMessage = "Preço antigo deve ser entre R$0.00 a R$9,999.99")]
        public decimal OldPrice { get; set; }

        [Display(Name = "Quantidade em estoque")]
        [Required(ErrorMessage = "Quantidade em estoque obrigatória")]
        [Range(-999999, 999999, ErrorMessage = "Quantidade em estoque deve ser de -999999 a 999999")]
        public int Quantity { get; set; }

        [Display(Name = "Produto Ativo")]
        [Required(ErrorMessage = "Produto ativo obrigatório")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Categoria obrigatória")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }
        public Category Categoria { get; set; }

        [Required(ErrorMessage = "Data de cadastro obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}
