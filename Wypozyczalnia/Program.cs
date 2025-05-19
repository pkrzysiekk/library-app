using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;
using Wypozyczalnia.Services;
using Wypozyczalnia.Validators;
using Mapster;
using Wypozyczalnia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Wypozyczalnia;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<LibraryContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;

            // password rules:
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.SignIn.RequireConfirmedAccount = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<LibraryContext>();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireElevatedPrivilleges",
                policy => policy.RequireRole("Admin", "Manager"));
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();

        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IBookService, BookService>();

        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientService, ClientService>();

        builder.Services.AddScoped<IRentalRepository, RentalRepository>();
        builder.Services.AddScoped<IRentalService, RentalService>();

        builder.Services.AddScoped<IValidator<RentalViewModel>, RentalValidator>();

        builder.Services.AddMapster();

        builder.Services.AddRazorPages();

        builder.Services.AddTransient<IEmailSender, CustomEmailSender>();

        MapsterConfig.RegisterMappings();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            var serviceProvider = scope.ServiceProvider;
            LibraryContext.Initialize(context);
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "User", "Manager" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}