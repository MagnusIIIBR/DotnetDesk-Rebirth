﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Helpdesk.Mvc.Data;
using Helpdesk.Mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helpdesk.Mvc.Services
{
	public interface IDotnetdesk
    {
        Task SendEmailBySendGridAsync(string apiKey, string fromEmail, string fromFullName, string subject, string message, string email);

        Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager);

        Task SendEmailByGmailAsync(string fromEmail,
            string fromFullName,
            string subject,
            string messageBody,
            string toEmail,
            string toFullName,
            string smtpUser,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            bool smtpSSL);

        Task CreateDefaultOrganization(string applicationUserId,
            ApplicationDbContext context);

        Task<string> UploadFile(List<IFormFile> files, IHostingEnvironment env);

    }
}
