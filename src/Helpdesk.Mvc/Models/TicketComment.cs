using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Mvc.Models
{
	public class TicketComment : BaseEntity
	{
		public Guid ticketThreadId { get; set; }

		[Required]
		[StringLength(300)]
		[Display(Name = "Comment")]
		public string Comment { get; set; }

		public Guid userCommentId { get; set; }
		public ApplicationUser userComment { get; set; }

		public virtual ICollection<File> files { get; set; }

		public Guid ticketId { get; set; }
		public Ticket ticket { get; set; }
	}
}
