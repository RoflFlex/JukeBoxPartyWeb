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
		var connectionString = builder.Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));
		builder.Services.AddDatabaseDeveloperPageExceptionFilter();
		builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
			.AddRoles<ApplicationRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultUI()
			.AddDefaultTokenProviders();
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
		if (!app.Environment.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			app.UseHsts();
		}

		app.UseStatusCodePages();

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
		app.Use(async (context, next) =>
		{
			await next.Invoke();
			//handle response
			//you may also need to check the request path to check whether it requests image
			if (context.User.Identity.IsAuthenticated)
			{
				var userName = context.User.Identity.Name;
				//retrieve uer by userName
				using (var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>())
				{
					var user = dbContext.Users.Where(u => u.UserName == userName).FirstOrDefault();
					user.LastAccessed = DateTime.Now;
					dbContext.Update(user);
					dbContext.SaveChanges();
				}
			}
		});
		app.MapHub<ChatHub>("/chatHub");
		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}");
		app.MapRazorPages();



		app.Run();
	}
}
