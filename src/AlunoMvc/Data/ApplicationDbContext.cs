using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Models;

namespace AlunoMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DemoMVC.Models.Aluno> Aluno { get; set; } = default!;
        public DbSet<DemoMVC.Models.Produto> Produto { get; set; } = default!;
    }
}
