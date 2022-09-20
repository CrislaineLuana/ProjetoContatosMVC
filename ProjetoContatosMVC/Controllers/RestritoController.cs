using Microsoft.AspNetCore.Mvc;
using ProjetoContatosMVC.Filters;

namespace ProjetoContatosMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
