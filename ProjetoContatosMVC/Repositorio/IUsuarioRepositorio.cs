using ProjetoContatosMVC.Models;
using System.Collections.Generic;

namespace ProjetoContatosMVC.Repositorio
{
    public interface IUsuarioRepositorio
    {
        List<UsuarioModel> BuscarTodos();
        UsuarioModel BuscarPorId(int id);
        UsuarioModel Atualizar(UsuarioModel usuario);
        UsuarioModel Adicionar(UsuarioModel usuario);
        bool Apagar(int id);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
        UsuarioModel BuscaPorLogin(string login);
        UsuarioModel BuscarPorEmailELogin(string email, string login);
    }
}
