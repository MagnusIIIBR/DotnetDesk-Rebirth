using System.Threading.Tasks;

namespace Helpdesk.Mvc.Services
{
	public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
