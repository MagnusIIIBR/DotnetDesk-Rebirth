﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Helpdesk.Mvc.Data;
using Helpdesk.Mvc.Models;

namespace Helpdesk.Mvc.Controllers.Api
{
	[Produces("application/json")]
    [Route("api/Config")]
    [Authorize]
    public class ConfigController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfigController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Config/ChangePersonalProfile
        [HttpPost("ChangePersonalProfile")]
        public async Task<IActionResult> ChangePersonalProfile([FromBody] ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ApplicationUser appUser = _context.ApplicationUser.Where(x => x.Id.Equals(applicationUser.Id)).FirstOrDefault();
                appUser.FullName = applicationUser.FullName;
                _context.Update(appUser);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Edit data success.", appUser });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }
    }
}