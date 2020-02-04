using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;

namespace src.Controllers
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