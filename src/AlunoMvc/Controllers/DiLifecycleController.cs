using AlunoMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlunoMvc.Controllers
{

    [Route("tedte-di")]
    public class DiLifecycleController : Controller
    {

        public IOperacao Operacao { get; set; }

        public DiLifecycleController(IOperacao operacao) {
            Operacao = operacao;
        }

        public IActionResult Index()
        {
            var teste = Operacao;

            return View();
        }
    }
}
