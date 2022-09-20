using Microsoft.AspNetCore.Mvc;
using ProjetoContatosMVC.Helper;
using ProjetoContatosMVC.Models;
using ProjetoContatosMVC.Repositorio;
using System;

namespace ProjetoContatosMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }


        public IActionResult Index()
        {
            //Se o usuárioe estiver logado, redirecionar para a Home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }


        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();

                        string mensagem = $"Sua nova senha é:{novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Redefinição de Senha", mensagem);

                        if(emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = "Enviamos uma nova senha para o e-mail cadastrado!";
                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Ocorreu um erro ao encaminhar sua nova senha por e-mail";
                            return View("RedefinirSenha", redefinirSenhaModel);
                        }
                   
                    }

                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha. Verifique os dados informados!";
                }
                return View("RedefinirSenha", redefinirSenhaModel);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops... Não conseguimos redefinir sua senha. Tente novamente. erro: {e.Message}";
                return View("RedefinirSenha", redefinirSenhaModel);
            }
        }


        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscaPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                                _sessao.CriarSessaoDoUsuario(usuario);
                                return RedirectToAction("Index", "Home");                                                
                        }

                        TempData["MensagemErro"] = $"Senha inválida! Tente novamente.";
                    }
                   
                    TempData["MensagemErro"] = $"Login ou/e senha inválidos! Tente novamente.";
                    
                    
                }
                return View("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops... Não conseguimos realizar seu login. Tente novamente. erro: {e.Message}";
                return View("Index");
            }
        }
    }
}
