using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Models;

namespace PharmacyManagement.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Rayon> Rayons { get; set; } = default!;

        public DbSet<Categorie> Categories { get; set; } = default!;

        public DbSet<Vente> Ventes { get; set; } = default!;

        public DbSet<Supplier> Suppliers { get; set; } = default!;

        public DbSet<Client> Clients { get; set; } = default!;

        public DbSet<Order> Orders { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<CategorieProduct>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Categories)
                .HasForeignKey(p => p.ProductId);
            builder.Entity<CategorieProduct>()
                .HasOne(p => p.Categorie)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategorieId);

            builder.Entity<VenteProduct>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Ventes)
                .HasForeignKey(p => p.ProductId);
            builder.Entity<VenteProduct>()
                .HasOne(p => p.Vente)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.VenteId);

            builder.Entity<OrderProduct>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.ProductId);
            builder.Entity<OrderProduct>()
                .HasOne(p => p.Order)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.OrderId);

            builder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            builder.Entity<Categorie>()
                .HasIndex(c=>c.Name)
                .IsUnique();

            builder.Entity<Order>()
                .HasIndex(p => p.Name) 
                .IsUnique();

            builder.Entity<Rayon>()
                .HasIndex(r=>r.Name)
                .IsUnique();


            base.OnModelCreating(builder);
        }
    }
}
