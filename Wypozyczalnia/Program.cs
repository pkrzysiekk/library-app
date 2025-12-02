using System.Globalization;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Wypozyczalnia.Data;
using Wypozyczalnia.Models;
using Wypozyczalnia.Models.ViewModels;
using Wypozyczalnia.Repository;
using Wypozyczalnia.Services;
using Wypozyczalnia.Validators;
using Wypozyczalnia.Validators.Password;
using Wypozyczalnia.Validators.reCAPTCHA;

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
        .AddPasswordValidator<AllCharactersUniqueValidator<IdentityUser>>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<LibraryContext>();

        builder.Services.Configure<ReCaptchaSettings>(builder.Configuration.GetSection("GoogleReCaptcha"));
        builder.Services.AddTransient<ReCaptchaValidator>();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireElevatedPrivilleges",
                policy => policy.RequireRole("Admin", "Manager"));
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

        builder.Services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var SupportedCultures = new[] {
                new CultureInfo("pl-PL"),
                new CultureInfo("en-US"),
            };
            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = SupportedCultures;
            options.SupportedUICultures = SupportedCultures;
        });
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();

        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IBookService, BookService>();

        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientService, ClientService>();

        builder.Services.AddScoped<IRentalRepository, RentalRepository>();
        builder.Services.AddScoped<IRentalService, RentalService>();

        builder.Services.AddScoped<IAuthRepository, AuthRepository>();

        builder.Services.AddScoped<IValidator<RentalViewModel>, RentalValidator>();
        builder.Services.AddScoped<IDashboardService, DashBoardService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
        });
        builder.Services.AddSession();

        builder.Services.AddMapster();

        builder.Services.AddRazorPages();

        builder.Services.AddTransient<IEmailSender, CustomEmailSender>();

        MapsterConfig.RegisterMappings();

        var app = builder.Build();
        app.UseRequestLocalization();
        app.UseSession();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            var serviceProvider = scope.ServiceProvider;
            LibraryContext.Initialize(context);
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "User", "Manager", "SuperUser","TrialAdmin" };

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