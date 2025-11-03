using Microsoft.AspNetCore.Mvc;

namespace AlunoMvc.Areas.Vendas.Controllers
{
    [Area("Produtos")]
    public class CadastroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View("Index");
        }
    }
}
