using AlunoMvc.Extensions;
using AlunoMvc.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace AlunoMvc.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
        {
            // Injeção de dependência
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IOperacao, Operacao>();
            builder.Services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            return builder;
        }
    }
}
