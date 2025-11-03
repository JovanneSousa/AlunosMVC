using Microsoft.AspNetCore.Mvc;

namespace AlunoMvc.Areas.Vendas.Controllers
{
    [Area("Vendas")]
    public class GestaoController : Controller
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
