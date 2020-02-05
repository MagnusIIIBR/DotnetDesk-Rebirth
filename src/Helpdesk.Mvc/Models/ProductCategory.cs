using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Mvc.Models
{
	public class ProductCategory : BaseEntity
	{
		public ProductCategory()
		{
			this.thumbUrl = "/images/no-image-available.png";
		}
		public Guid productCategoryId { get; set; }
		[Display(Name = "Product Name")]
		[Required]
		[StringLength(100)]
		public string categoryName { get; set; }
		[StringLength(200)]
		[Display(Name = "Description")]
		public string description { get; set; }
		[StringLength(255)]
		[Display(Name = "Thumb URL")]
		public string thumbUrl { get; set; }


		public Guid organizationId { get; set; }
		public Organization organization { get; set; }
		//Products
		public ICollection<Product> products { get; set; }

	}
}
