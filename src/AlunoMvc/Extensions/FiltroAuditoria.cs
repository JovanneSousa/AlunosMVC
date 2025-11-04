using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace AlunoMvc.Extensions
{
    public class FiltroAuditoria : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string message = context.HttpContext.User.Identity.Name + " Acessou: " +
                    context.HttpContext.Request.GetDisplayUrl();
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // throw new NotImplementedException();
        }
    }
}
