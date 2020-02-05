using System;
using System.Linq;
using System.Threading.Tasks;
using Helpdesk.Mvc.Data;
using Helpdesk.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace src.Controllers.Api
{
	[Produces("application/json")]
	[Route("api/ProductCategory")]
	[Authorize]
	public class ProductCategoryController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProductCategoryController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/ProductCategory
		[HttpGet("{organizationId}")]
		public IActionResult GetProductCategory([FromRoute]Guid organizationId)
		{
			return Json(new { data = _context.ProductCategory.Where(x => x.organizationId.Equals(organizationId)).ToList() });
		}



		// POST: api/ProductCategory
		[HttpPost]
		public async Task<IActionResult> PostProductCategory([FromBody] ProductCategory productCategory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				if (productCategory.productCategoryId == Guid.Empty)
				{
					productCategory.productCategoryId = Guid.NewGuid();
					_context.ProductCategory.Add(productCategory);

					await _context.SaveChangesAsync();

					return Json(new { success = true, message = "Add new data success." });
				}
				else
				{
					_context.Update(productCategory);

					await _context.SaveChangesAsync();

					return Json(new { success = true, message = "Edit data success." });
				}
			}
			catch (Exception ex)
			{

				return Json(new { success = false, message = ex.Message });
			}


		}

		// DELETE: api/ProductCategory/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductCategory([FromRoute] Guid id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var productCategory = await _context.ProductCategory.SingleOrDefaultAsync(m => m.productCategoryId == id);
				if (productCategory == null)
				{
					return NotFound();
				}

				_context.ProductCategory.Remove(productCategory);
				await _context.SaveChangesAsync();

				return Json(new { success = true, message = "Delete success." });
			}
			catch (Exception ex)
			{

				return Json(new { success = false, message = ex.Message });
			}


		}

		private bool ProductCategoryExists(Guid id)
		{
			return _context.ProductCategory.Any(e => e.productCategoryId == id);
		}
	}
}