using ProjetoContatosMVC.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoContatosMVC.Models
{
    public class UsuarioSemSenhaModel
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

    }
}
