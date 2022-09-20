using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoContatosMVC.Data;
using ProjetoContatosMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoContatosMVC.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly bancoContext _bancoContext;
        public UsuarioRepositorio(bancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
             usuario.DataCadastro = DateTime.Now;
             usuario.setSenhaHash();
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();

            return usuario;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = BuscarPorId(alterarSenhaModel.Id);
            if (usuarioDB == null) throw new Exception("Houve um erro na atualização da Senha");

            if(!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere");

            if(usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual");

            usuarioDB.setNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;

        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDb = BuscarPorId(id);

            if (usuarioDb == null) throw new System.Exception("Houve um erro na atualização do registro!");

            _bancoContext.Usuarios.Remove(usuarioDb);
            _bancoContext.SaveChanges();

            return true;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = BuscarPorId(usuario.Id);

            if (usuarioDb == null) throw new System.Exception("Houve um erro na atualização do registro!");

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.Login = usuario.Login;
            usuarioDb.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDb);
            _bancoContext.SaveChanges();

            return usuarioDb;
        }

        public UsuarioModel BuscaPorLogin(string login)
        {
            UsuarioModel usuario = _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
            return usuario;
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            UsuarioModel usuario = _bancoContext.Usuarios
                                   .FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && 
                                                        x.Login.ToUpper() == login.ToUpper());

            return usuario;
        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios
                .Include(x => x.Contatos)
                .ToList();
        }
    }
}
