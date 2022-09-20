using System.ComponentModel.DataAnnotations;

namespace ProjetoContatosMVC.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite seu login!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite seu e-mail!")]
        [EmailAddress(ErrorMessage = "O e-mail inserido está inválido!")]
        public string Email { get; set; }



    }
}
