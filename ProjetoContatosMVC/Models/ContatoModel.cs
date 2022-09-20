using System.ComponentModel.DataAnnotations;

namespace ProjetoContatosMVC.Models
{
	public class ContatoModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Digite um nome!")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "Digite um e-mail!")]
		[EmailAddress(ErrorMessage = "O e-mail informado está inválido!")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Digite um número de celular!")]
		[Phone(ErrorMessage = "O celular informado está inválido!")]
		public string Celular { get; set; }

        public int? UsuarioID { get; set; }
        public UsuarioModel Usuario { get; set; }
    }

}
