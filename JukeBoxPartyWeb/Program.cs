using JukeBoxPartyWeb.Data;
using JukeBoxPartyWeb.Models;
using JukeBoxPartyWeb.Services;
using JukeBoxPartyWeb.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
        //The following code changes all data protection tokens timeout period to 3 hours:
        builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
               o.TokenLifespan = TimeSpan.FromHours(3));
        //The default inactivity timeout is 14 days. The following code sets the inactivity timeout to 5 days:
        builder.Services.ConfigureApplicationCookie(o =>
        {
            o.ExpireTimeSpan = TimeSpan.FromDays(5);
            o.SlidingExpiration = true;
        });

        builder.Services.AddSignalR();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers.Append(
                     "Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
            }
        });

        app.UseRouting();

        app.UseAuthorization();

        app.MapHub<ChatHub>("/chatHub");
        //app.MapHub<QueueHub>("/queueHub");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Song}/{action=Create}");
        app.MapRazorPages();
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "AccountManager", "SongManager", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string email = "admin@admin.com";
            string password = "Test1234!";

            if(await userManager.FindByEmailAsync(email) == null){
                var user = new IdentityUser();
                user.UserName = email;
                user.Email = email;
                user.EmailConfirmed = true;
                await userManager.CreateAsync(user,password);
            }
        }


        app.Run();
    }
}