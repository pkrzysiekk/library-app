using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;
using Wypozyczalnia.Services;
using Wypozyczalnia.Validators;

namespace Wypozyczalnia;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<LibraryContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();

            LibraryContext.Initialize(context);
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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}