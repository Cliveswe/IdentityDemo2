using IdentityDemo.Application.Cars.Interfaces;
using IdentityDemo.Application.Cars.Services;
using IdentityDemo.Application.Users.Interfaces;
using IdentityDemo.Application.Users.Services;
using IdentityDemo.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IdentityDemo.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IIdentityUserService, IdentityUserService>();
        builder.Services.AddTransient<ICarService, CarService>();

        // Konfigurera EF
        builder.Services.AddDbContext<ApplicationContext>(
            o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Identity: Registera identity-klasserna och vilken DbContext som ska användas
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Här kan vi (om vi vill) ange inställningar för t.ex. lösenord
            // (ofta struntar man i detta och kör på default-värdena)
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
        }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

        // Identity: Hit ska icke inloggade användare skikas (om de besöker skyddade sidor)
        builder.Services.ConfigureApplicationCookie(o => {
            o.LoginPath = "/login";
            o.LogoutPath = "/logout";
        });

        var app = builder.Build();

        // Identity: Behörighet
        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}