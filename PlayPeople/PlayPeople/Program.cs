using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlayPeople.Data;

namespace PlayPeople
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Games}");

            app.MapControllerRoute(
                name: "games",
                pattern: "games",
                defaults: new { controller = "Home", action = "Games" });

            app.MapControllerRoute(
                name: "news",
                pattern: "news",
                defaults: new { controller = "Home", action = "News" });

            app.MapControllerRoute(
                name: "read",
                pattern: "read",
                defaults: new { controller = "Home", action = "Read" });

            app.MapControllerRoute(
                name: "releases",
                pattern: "releases",
                defaults: new { controller = "Home", action = "Releases" });

            app.MapControllerRoute(
                name: "support",
                pattern: "support",
                defaults: new { controller = "Home", action = "Support" });

            app.MapControllerRoute(
                name: "login",
                pattern: "login",
                defaults: new { controller = "Home", action = "Login" });

             app.MapControllerRoute(
                name: "register",
                pattern: "register",
                defaults: new { controller = "Home", action = "Register" });

            app.Run();
        }
    }
}
