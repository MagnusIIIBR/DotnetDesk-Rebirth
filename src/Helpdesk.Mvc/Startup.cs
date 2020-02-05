using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Helpdesk.Mvc.Data;
using Helpdesk.Mvc.Models;
using Helpdesk.Mvc.Services;
using Microsoft.Extensions.Hosting;

namespace Helpdesk.Mvc
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				// Password settings
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 1;

				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				// User settings
				options.User.RequireUniqueEmail = true;

				// email confirmation require
				options.SignIn.RequireConfirmedEmail = false;
			})
			    .AddEntityFrameworkStores<ApplicationDbContext>()
			    .AddDefaultTokenProviders();

			// cookie settings
			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromDays(150);
				options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
				options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
				options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
				options.SlidingExpiration = true;
			});

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			// Add DI for Dotnetdesk
			services.AddTransient<IDotnetdesk, Dotnetdesk>();

			// Get SendGrid configuration options
			services.Configure<SendGridOptions>(Configuration.GetSection("SendGridOptions"));

			// Get SMTP configuration options
			services.Configure<SmtpOptions>(Configuration.GetSection("SmtpOptions"));

			//Inject ApplicationInsights
			services.AddApplicationInsightsTelemetry(Configuration);

			services.AddMvc();

			services.AddControllers().AddNewtonsoftJson();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				//app.UseExceptionHandler("/Home/Error");
				app.UseExceptionHandler(Helpdesk.Mvc.MVC.Errors.Error500.FullUrl);
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.Use(async (ctx, next) =>
			{
				await next();

				if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
				{
					//Re-execute the request so the user gets the error page
					string originalPath = ctx.Request.Path.Value;
					ctx.Items["originalPath"] = originalPath;
					ctx.Request.Path = "/error/404";
					await next();
				}
			});

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=" + MVC.Pages.ConfigIndex.Controller + "}/{action=" + MVC.Pages.ConfigIndex.Action + "}/{id?}");
			});
		}
	}
}
