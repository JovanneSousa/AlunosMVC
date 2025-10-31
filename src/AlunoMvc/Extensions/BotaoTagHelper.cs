using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AlunoMvc.Extensions
{
    [HtmlTargetElement("*", Attributes = "tipo-botao, route-id")]
    public class BotaoTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public BotaoTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("tipo-botao")]
        public TipoBotao TipoBotaoSelecao { get; set; }

        [HtmlAttributeName("route-id")]
        public int RouteId { get; set; }

        private string NomeAction;
        private string NomeClasse;
        private string SpanIcone;

        public override void Process (TagHelperContext context, TagHelperOutput output)
        {
            switch (TipoBotaoSelecao)
            {
                case TipoBotao.Detalhes:
                    NomeAction = "detalhes";
                    NomeClasse = "btn btn-info";
                    SpanIcone = "fa fa-search";
                    break;
                case TipoBotao.Editar:
                    NomeAction = "editar";
                    NomeClasse = "btn btn-warning";
                    SpanIcone = "fa fa-pencil-alt";
                    break;
                case TipoBotao.Excluir:
                    NomeAction = "deletar";
                    NomeClasse = "btn btn-danger";
                    SpanIcone = "fa fa-trash";
                    break;
            }

            string controller = _contextAccessor.HttpContext.GetRouteData().Values["controller"].ToString().ToLower();

            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"{controller}/{NomeAction}/{RouteId}");
            output.Attributes.SetAttribute("class", NomeClasse);

            var IconSpan = new TagBuilder("span");
            IconSpan.AddCssClass(SpanIcone);

            output.Content.AppendHtml(IconSpan);
        }
    }

    public enum TipoBotao
    {
        Detalhes = 1,
        Editar,
        Excluir
    }
}
