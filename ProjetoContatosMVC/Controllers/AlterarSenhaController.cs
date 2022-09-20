using Microsoft.AspNetCore.Mvc;
using ProjetoContatosMVC.Helper;
using ProjetoContatosMVC.Models;
using ProjetoContatosMVC.Repositorio;
using System;

namespace ProjetoContatosMVC.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();

                    alterarSenhaModel.Id = usuario.Id;

                    _usuarioRepositorio.AlterarSenha(alterarSenhaModel);

                    TempData["MensagemSucesso"] = "Sua senha foi alterada com sucesso!";
                    return View("Index", alterarSenhaModel);
                }
                else
                {
                    TempData["MensagemSucesso"] = "Sua senha foi alterada com sucesso!";
                    return View("Index", alterarSenhaModel);
                }



            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = $"Ops.. Ocorre um erro ao tentar altera sua senha. Tente Novamente! erro: {ex.Message}";
                return View("Index", alterarSenhaModel);
            }
        }

    }
}
