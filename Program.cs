
using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Data;
using PharmacyManagement.Interfaces;
using PharmacyManagement.Models;
using PharmacyManagement.Services;

namespace PharmacyManagement;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
        });

        builder.Services.AddTransient<IUserRepository,UserRepository>();
        builder.Services.AddTransient<IStoreManager<Client>,ClientRepository>();
        builder.Services.AddTransient<IStoreManager<Product>, ProductRepository>();
        builder.Services.AddTransient<IStoreManager<Order>, OrderRepository>();
        builder.Services.AddTransient<IStoreManager<Categorie>, CategorieRepository>();
        builder.Services.AddTransient<IStoreManager<Vente>, VenteRepository>();
        builder.Services.AddTransient<IStoreManager<Rayon>, RayonRepository>();
        builder.Services.AddTransient<IStoreManager<Supplier>, SupplierRepository>();


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
