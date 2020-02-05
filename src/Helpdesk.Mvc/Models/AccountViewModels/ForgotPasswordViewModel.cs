using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Mvc.Models.AccountViewModels
{
	public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
