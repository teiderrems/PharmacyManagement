
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

        builder.Services
                .AddAuthentication();
                 //.AddBearerToken(IdentityConstants.BearerScheme);
        builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = true;
        })
                .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddTransient<IUserRepository,UserRepository>();
        builder.Services.AddTransient<IStoreManager<Client>,ClientRepository>();
        builder.Services.AddTransient<IStoreManager<Product>, ProductRepository>();
        builder.Services.AddTransient<IStoreManager<Order>, OrderRepository>();
        builder.Services.AddTransient<IStoreManager<Categorie>, CategorieRepository>();
        builder.Services.AddTransient<IStoreManager<Vente>, VenteRepository>();
        builder.Services.AddTransient<IStoreManager<Rayon>, RayonRepository>();
        builder.Services.AddTransient<IStoreManager<Supplier>, SupplierRepository>();

        // Add a CORS policy for the client
        // Add .AllowCredentials() for apps that use an Identity Provider for authn/z
        builder.Services.AddCors(
            options => options.AddPolicy(
                "wasm",
                policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "http://localhost:54512",
            builder.Configuration["FrontendUrl"] ?? "https://localhost:7230"])
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

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

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapIdentityApi<ApplicationUser>();
        app.MapControllers();

        app.Run();
    }
}
