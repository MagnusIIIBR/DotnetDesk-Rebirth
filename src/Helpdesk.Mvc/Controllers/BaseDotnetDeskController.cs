﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Helpdesk.Mvc.Data;
using Helpdesk.Mvc.Models;

namespace Helpdesk.Mvc.Controllers
{
    public class BaseDotnetDeskController : Controller
    {
        protected bool IsHaveEnoughAccessRight()
        {
            //todo: cek controller
            //todo: cek user
            return true;
        }

    }
}