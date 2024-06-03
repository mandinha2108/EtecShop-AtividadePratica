using EtecShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EtecShop.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        #region Populando os dados da Gestão de Usuários
        List<IdentityRole> roles = new()
        {
            new IdentityRole()
            {
                 Id = Guid.NewGuid().ToString(),
                 Name = "Administrador",
                 NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Funcionário",
                NormalizedName = "FUNCIONÁRIO"
            },
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Cliente",
                NormalizedName = "CLIENTE"
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);

        IdentityUser user = new()
        {
            Id = Guid.NewGuid().ToString(),
            Email = "admin@etecshop.com",
            NormalizedEmail = "ADMIN@ETECSHOP.COM",
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            LockoutEnabled = true,
            EmailConfirmed = true,
        };
        PasswordHasher<IdentityUser> pass = new();
        user.PasswordHash = pass.HashPassword(user, "@Etec123");
        builder.Entity<IdentityUser>().HasData(user);
        List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>() {
                UserId = user.Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>() {
                UserId = user.Id,
                RoleId = roles[1].Id
            },
            new IdentityUserRole<string>() {
                UserId = user.Id,
                RoleId = roles[2].Id
            }};
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion

        #region Popular Categorias
        List<Categoria> categorias = new(){
            new Categoria() {
                Id = 1,
                Nome = "Livros e papelaria"
            },
            new Categoria() {
                Id = 2,
                Nome = "Games e PC Gamer"
            },
            new Categoria() {
                Id = 3,
                Nome = "Informática"
            },
            new Categoria() {
                Id = 4,
                Nome = "Smartphones"
            },
            new Categoria() {
                Id = 5,
                Nome = "Eletrodomesticos e Casa"
            },
            new Categoria() {
                Id = 6,
                Nome = "Beleza e Perfumaria"
            },
            new Categoria() {
                Id = 7,
                Nome = "Móveis e Decoração"
            }
        };
        builder.Entity<Categoria>().HasData(categorias);
        #endregion
    }
}
