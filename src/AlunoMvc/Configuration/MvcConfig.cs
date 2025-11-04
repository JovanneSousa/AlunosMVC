using AlunoMvc.Data;
using DemoMVC.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace AlunoMvc.Configuration
{
    public static class MvcConfig
    {

        public static WebApplicationBuilder AddMvcConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
                .AddEnvironmentVariables();

            builder.Services.AddControllersWithViews(o =>
            {
                o.Filters.Add(
                new AutoValidateAntiforgeryTokenAttribute());

                MvcOptionsConfig.ConfigurarMensagensDeModelBinding(o.ModelBindingMessageProvider);
            });

            // Adicionando suporte a mudança de convenção da rota das areas.
            builder.Services.Configure<RazorViewEngineOptions>(o =>
            {
                o.AreaPageViewLocationFormats.Clear();
                o.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
                o.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                o.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Adiciona o contexto db
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddHsts(o =>
            {
                o.Preload = true;
                o.IncludeSubDomains = true;
                o.MaxAge = TimeSpan.FromDays(60);
                o.ExcludedHosts.Add("example.com");
                o.ExcludedHosts.Add("www.example.com");
            });

            builder.Services.Configure<ApiConfig>(
                builder.Configuration.GetSection(ApiConfig.ConfigName));

            return builder;
        }

        public static WebApplication UseMvcConfiguration(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("erro/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseGlobalizationConfig();

            // força o redirecionamento https
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "areas",
            //    pattern: "{area=exists}/{controller=Home}/{action=Index}/{id?}");

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            return app;
        }
    }
}
