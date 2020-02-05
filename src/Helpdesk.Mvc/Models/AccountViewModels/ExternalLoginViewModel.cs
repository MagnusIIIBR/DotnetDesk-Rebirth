using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Mvc.Models.AccountViewModels
{
	public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
