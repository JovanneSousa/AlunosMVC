using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace AlunoMvc.Configuration
{
    public static class GlobalizationConfig
    {
        public static WebApplication UseGlobalizationConfig(this WebApplication app)
        {
            CultureInfo defaultCulture = new CultureInfo("pt-BR");

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };

            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}
