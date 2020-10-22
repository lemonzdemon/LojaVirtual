using BiaBraga.Dominio.Enums;
using System;

namespace BiaBraga.Dominio.Entidades
{
    public class Usuario 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Imagem { get; set; }
        public DateTime Nascimento { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneMovel { get; set; }
        public string Email { get; set; }
        public bool ReceberMensagem { get; set; }
        public bool ReceberEmail { get; set; }
        public DateTime DataCadastro { get; set; }
        public Role Role { get; set; }
    }
}
