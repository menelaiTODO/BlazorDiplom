using BlazorDiplom.Middleware;
using BlazorSpinner;
using DevExpress.Blazor;
using IdentityServerCore.DbContext;
using IdentityServerCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OLTPDatabaseCore.DatabaseContext;
using OLTPDatabaseCore.Infrastructure;

internal static class Program
{
    internal static void Main(string[] args)
    {
        var seed = args.Contains("/seed");

        if (seed)
        {
            args = args.Except(new[] { "/seed" }).ToArray();
        }

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
        builder.Services.AddServerSideBlazor();
        builder.Services.AddDbContext<IdentityAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));
        builder.Services.AddDbContext<OLTPDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OLTP")));

        builder.Services.AddIdentity<User, IdentityRole>(options => { 
            options.SignIn.RequireConfirmedAccount = false; 
            options.User.RequireUniqueEmail = true; 
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<IdentityAppDbContext>();

        builder.Services.AddScoped<SpinnerService>();

        builder.Services.AddScoped<UserManager<User>>();
        builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = BootstrapVersion.v5);

        builder.Services.AddSingleton<DataSeeder>();

        builder.WebHost.UseWebRoot("wwwroot");
        builder.WebHost.UseStaticWebAssets();

        var app = builder.Build();

        app.UseMiddleware<AuthMiddleWare>();

        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<OLTPDbContext>();
            var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

            db.Database.Migrate();

            if (seed)
            {
                dataSeeder.SeedData(db);

                Console.WriteLine("Данные сгенерированы");
                
                return;
            }
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        app.MapControllers();

        app.Run();
    }
}