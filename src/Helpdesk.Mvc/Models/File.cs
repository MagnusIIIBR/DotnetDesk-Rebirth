using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Mvc.Models
{
	public class File : BaseEntity
	{
		public Guid fileId { get; set; }

		[Required]
		[StringLength(300)]
		public string Path { get; set; }

		public Guid userUploaderId { get; set; }
		public ApplicationUser userUploader { get; set; }

		public Ticket ticket { get; set; }
	}
}
