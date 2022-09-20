using Microsoft.AspNetCore.Mvc;
using ProjetoContatosMVC.Filters;
using ProjetoContatosMVC.Models;
using ProjetoContatosMVC.Repositorio;
using System;
using System.Collections.Generic;

namespace ProjetoContatosMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    [PaginaParaUsuarioAdm]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio;

		public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IContatoRepositorio contatoRepositorio)
		{
            _usuarioRepositorio = usuarioRepositorio;
            _contatoRepositorio = contatoRepositorio;
		}


        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();  
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult ListarContatosPorUsuarioId(int Id)
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(Id);
            return PartialView("_ContatosUsuario", contatos);
        }

        public IActionResult Apagar(int id)
        {

            try
            {
                UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
                
                bool apagado = _usuarioRepositorio.Apagar(usuario.Id);
                if (apagado)
                {
                    TempData["MenssagemErro"] = "O usuário foi excluido com sucesso";
                    return RedirectToAction("Index");
                }

                TempData["MenssagemErro"] = "Ops, usuário não existe!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops... Ocorreu um erro no momento de apagar este usuário. erro {ex.Message}";
                return RedirectToAction("Index");
            }


        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
		{
            try
            {
                if (ModelState.IsValid)
                {


                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops... Ocorreu um erro ao tentar cadastrar o usuário! Erro:{e.Message}";
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {

            try
            {
                UsuarioModel usuario = null;

                if(ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Perfil = usuarioSemSenhaModel.Perfil,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email
                    };

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "O usuário foi alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar",usuario);

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops... Ocorreu um erro ao tentar editar o usuário! erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        


    }
}
