using System;
using System.Linq;
using Helpdesk.Mvc.Data;
using Helpdesk.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.Mvc.Controllers
{
	public class ProductCategoryController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductCategoryController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index(Guid org)
		{
			if (org == Guid.Empty)
			{
				return NotFound();
			}
			Organization organization = _context.Organization.Where(x => x.organizationId.Equals(org)).FirstOrDefault();
			ViewData["org"] = org;
			return View(organization);
		}

		public IActionResult AddEdit(Guid org, Guid id)
		{
			if (id == Guid.Empty)
			{
				ProductCategory productCategory = new ProductCategory();
				productCategory.organizationId = org;
				return View(productCategory);
			}
			else
			{
				return View(_context.ProductCategory.Where(x => x.productCategoryId.Equals(id)).FirstOrDefault());
			}

		}
	}
}