using ProjetoContatosMVC.Models;

namespace ProjetoContatosMVC.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void RemoverSessaoDoUsuario();
        UsuarioModel BuscarSessaoDoUsuario();

    }
}
