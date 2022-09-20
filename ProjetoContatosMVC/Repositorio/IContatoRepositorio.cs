using ProjetoContatosMVC.Models;
using System.Collections.Generic;

namespace ProjetoContatosMVC.Repositorio
{
	public interface IContatoRepositorio
	{
		List<ContatoModel> BuscarTodos(int UsuarioId);
		ContatoModel listarPorId(int id);
		bool Apagar(int id);
		ContatoModel Atualizar(ContatoModel contato);
		ContatoModel Adicionar(ContatoModel contato);
		

	}
}
