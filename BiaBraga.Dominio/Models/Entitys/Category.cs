﻿using System.ComponentModel.DataAnnotations;

namespace BiaBraga.Domain.Models.Entitys
{
    public class Category
    {
        [Display(Name = "Codigo da categoria")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Nome deve ter um tamanho de 5 a 20 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "Descrição deve ter no máximo 200 caracteres")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Departamento é obrigatório.")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}