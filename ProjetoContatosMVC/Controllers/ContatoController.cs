using Microsoft.AspNetCore.Mvc;
using ProjetoContatosMVC.Filters;
using ProjetoContatosMVC.Helper;
using ProjetoContatosMVC.Models;
using ProjetoContatosMVC.Repositorio;
using System;
using System.Collections.Generic;

namespace ProjetoContatosMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        readonly private IContatoRepositorio _contatoRepositorio;
        readonly private ISessao _sessao;

		public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
		{
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
		}


        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.listarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.listarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                _contatoRepositorio.Apagar(id);
                TempData["MensagemSucesso"] = "Contato deletado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops... Ocorreu um erro no momento de deletar o contato! Erro:{e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioID = usuario.Id;
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = $"Ops... Ocorreu um erro ao tentar cadastrar o contato! Erro:{e.Message}";
                return RedirectToAction("Index");
            }
            
                
		}


        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioID = usuario.Id;
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);
            }
            catch(Exception e) 
            {
                TempData["MensagemErro"] = $"Ops... Ocorreu um erro ao tentar alterar o contato! Erro:{e.Message}";
                return RedirectToAction("Index");
            }
            
        }

        


    }
}
