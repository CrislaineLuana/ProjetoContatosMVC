using ProjetoContatosMVC.Data;
using ProjetoContatosMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoContatosMVC.Repositorio
{
	public class ContatoRepositorio : IContatoRepositorio
	{
		readonly private bancoContext _bancoContext;

		public ContatoRepositorio(bancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public List<ContatoModel> BuscarTodos(int UsuarioId)
		{

			return _bancoContext.Contatos.Where(x => x.UsuarioID == UsuarioId).ToList();
			
		}

		public ContatoModel listarPorId(int id)
		{
			return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
		}

		public ContatoModel Adicionar(ContatoModel contato)
		{
			// gravar no banco de dados
			_bancoContext.Contatos.Add(contato);
			_bancoContext.SaveChanges();

			return contato;
		}

        public ContatoModel Atualizar(ContatoModel contato)
        {
			ContatoModel contatoDb = listarPorId(contato.Id);

			if (contatoDb == null) throw new System.Exception("Houve um erro na atualização do registro!");

			contatoDb.Nome = contato.Nome;
			contatoDb.Email = contato.Email;
			contatoDb.Celular = contato.Celular;

			_bancoContext.Contatos.Update(contatoDb);
			_bancoContext.SaveChanges();

			return contatoDb;
        }

        public bool Apagar(int id)
        {
			ContatoModel contatoDb = listarPorId(id);

			if (contatoDb == null) throw new System.Exception("Houve um erro ao tentar deletar o registro!");

			_bancoContext.Contatos.Remove(contatoDb);
			_bancoContext.SaveChanges();

			return true;
		}
    }
}
