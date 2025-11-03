using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis;

namespace AlunoMvc.Extensions
{
    [HtmlTargetElement("*", Attributes = "tipo-botao, route-id")]
    public class BotaoTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly LinkGenerator _linkGenerator;

        public BotaoTagHelper(IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator)
        {
            _contextAccessor = contextAccessor;
            _linkGenerator = linkGenerator;
        }

        [HtmlAttributeName("tipo-botao")]
        public TipoBotao TipoBotaoSelecao { get; set; }

        [HtmlAttributeName("route-id")]
        public int RouteId { get; set; }

        private string nomeAction;
        private string nomeClasse;
        private string spanIcone;

        public override void Process (TagHelperContext context, TagHelperOutput output)
        {
            switch (TipoBotaoSelecao)
            {
                case TipoBotao.Detalhes:
                    nomeAction = "detalhes";
                    nomeClasse = "btn btn-info";
                    spanIcone = "fa fa-search";
                    break;
                case TipoBotao.Editar:
                    nomeAction = "editar";
                    nomeClasse = "btn btn-warning";
                    spanIcone = "fa fa-pencil-alt";
                    break;
                case TipoBotao.Excluir:
                    nomeAction = "excluir";
                    nomeClasse = "btn btn-danger";
                    spanIcone = "fa fa-trash";
                    break;
            }

            var endpoint = _contextAccessor.HttpContext?.GetEndpoint();
            var controller = endpoint.Metadata
                .OfType<Microsoft.AspNetCore.Mvc.RouteAttribute>()
                .FirstOrDefault()?.Template;

            var host = $"{_contextAccessor.HttpContext.Request.Scheme}://" +
                $"{_contextAccessor.HttpContext.Request.Host.Value}";

            var indexPath = _linkGenerator.GetPathByAction(
                _contextAccessor.HttpContext,
                nomeAction,
                controller,
                values: new { id = RouteId }
                )!;

            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"{host}{indexPath}");
            output.Attributes.SetAttribute("class", nomeClasse);

            var iconSpan = new TagBuilder("span");
            iconSpan.AddCssClass(spanIcone);

            output.Content.AppendHtml(iconSpan);
        }
    }

    public enum TipoBotao
    {
        Detalhes = 1,
        Editar,
        Excluir
    }
}
