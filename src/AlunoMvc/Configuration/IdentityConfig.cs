using AlunoMvc.Data;
using Microsoft.AspNetCore.Identity;

namespace AlunoMvc.Configuration
{
    public static class IdentityConfig
    {

        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return builder;
        }
    }
}
