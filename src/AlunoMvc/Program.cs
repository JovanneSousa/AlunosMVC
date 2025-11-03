using AlunoMvc.Data;
using AlunoMvc.Extensions;
using AlunoMvc.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddRouting(options =>
//    options.ConstraintMap["slugify"] = typeof(RouteSlugifyParameterTransformer));

// Injeção de dependência
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IOperacao, Operacao>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Adiciona o contexto db
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews( o =>
{
    o.Filters.Add(
    new AutoValidateAntiforgeryTokenAttribute());
});

// Adicionando suporte a mudança de convenção da rota das areas.
builder.Services.Configure<RazorViewEngineOptions>(o =>
{
    o.AreaPageViewLocationFormats.Clear();
    o.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
    o.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
    o.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area=exists}/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
