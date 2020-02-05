using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Helpdesk.Mvc.Data;
using Helpdesk.Mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Helpdesk.Mvc.Controllers
{
	public class ProductController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductController(ApplicationDbContext context)
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

			IList<ProductCategory> productCategories = _context.ProductCategory.Where(x => x.organizationId.Equals(org)).ToList();
			ViewBag.productCategoryId = new SelectList(productCategories, "productCategoryId", "categoryName");

			ViewData["org"] = org;
			return View(organization);
		}

		public IActionResult AddEdit(Guid org, Guid id)
		{
			if (id == Guid.Empty)
			{
				Product product = new Product();
				product.organizationId = org;

				IList<ProductCategory> productCategories = _context.ProductCategory.Where(x => x.organizationId.Equals(org)).ToList();
				ViewBag.productCategoryId = new SelectList(productCategories, "productCategoryId", "categoryName");

				return View(product);
			}
			else
			{
				Product product = _context.Product.Where(x => x.productId.Equals(id)).FirstOrDefault();

				IList<ProductCategory> productCategories = _context.ProductCategory.Where(x => x.organizationId.Equals(product.organizationId)).ToList();
				ViewBag.productCategoryId = new SelectList(productCategories, "productCategoryId", "categoryName", product.productCategoryId);

				return View(product);
			}

		}
	}
}