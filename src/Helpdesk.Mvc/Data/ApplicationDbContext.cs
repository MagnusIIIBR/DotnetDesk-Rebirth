using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Helpdesk.Mvc.Models;
using System.Linq;

namespace Helpdesk.Mvc.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		    : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
			foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

		}

		public DbSet<Helpdesk.Mvc.Models.Organization> Organization { get; set; }

		public DbSet<Helpdesk.Mvc.Models.Product> Product { get; set; }

		public DbSet<Helpdesk.Mvc.Models.ProductCategory> ProductCategory { get; set; }

		public DbSet<Helpdesk.Mvc.Models.Customer> Customer { get; set; }

		public DbSet<Helpdesk.Mvc.Models.Contact> Contact { get; set; }

		public DbSet<Helpdesk.Mvc.Models.SupportAgent> SupportAgent { get; set; }

		public DbSet<Helpdesk.Mvc.Models.SupportEngineer> SupportEngineer { get; set; }

		public DbSet<Helpdesk.Mvc.Models.Ticket> Ticket { get; set; }

		public DbSet<Helpdesk.Mvc.Models.ApplicationUser> ApplicationUser { get; set; }
	}
}
