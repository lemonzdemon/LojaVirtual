using BiaBraga.Domain.Enums;
using BiaBraga.Domain.Models.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BiaBraga.Domain.Models.Dtos
{
    public class UserViewDto
    {
        [Display(Name = "Codigo do usuario")]
        public int Id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Nome deve ter um tamanho de 5 a 100 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Apelido")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Apelido deve ter um tamanho de 5 a 20 caracteres")]
        public string Nick { get; set; }

        [Display(Name = "Sobre mim")]
        [StringLength(1000, ErrorMessage = "Sobre mim tem um limite de 500 caracteres")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Gênero obrigatório")]
        public int GenerId { get; set; }
        public Genre Gener { get; set; }

        [Display(Name = "Senha de acesso")]
        [Required(ErrorMessage = "Senha obrigatória")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Senha deve ter um tamanho de 5 a 50 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmação da senha")]
        [Required(ErrorMessage = "Confirmação da senha obrigatória")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Senhas não conferem")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "CPF OU CNPJ")]
        [StringLength(14)]
        public string CPF { get; set; }

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime Birth { get; set; }

        [Display(Name = "Telefone Fixo")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(14, ErrorMessage = "Telefone Fixo deve ter no maximo 14 caracteres")]
        public string Telephone { get; set; }

        [Display(Name = "Celular")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(14, ErrorMessage = "Celular deve ter no maximo 14 caracteres")]
        public string CellPhone { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Imagem de perfil")]
        [MaxLength(20)]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Receber novidades por celular")]
        public bool ReceiveCellPhoneMessage { get; set; }

        [Required]
        [Display(Name = "Receber novidades por email")]
        public bool ReceiveEmailMessage { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data de cadastro")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Função do usuário")]
        public Role Role { get; set; }
    }
}
