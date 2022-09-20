using ProjetoContatosMVC.Enums;
using ProjetoContatosMVC.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoContatosMVC.Models
{
    public class UsuarioModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite um nome!")]

        public string Nome { get; set; }
        [Required(ErrorMessage="Digite um login!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite um e-mail!")]
        [EmailAddress(ErrorMessage = "O e-mail digitado está inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Selecione um perfil!")]

        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Digite uma senha!")]

        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public virtual List<ContatoModel> Contatos { get; set; }
        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void setSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public void setNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();

            return novaSenha;
        }
    }
}
