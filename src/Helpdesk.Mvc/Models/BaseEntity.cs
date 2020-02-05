﻿using System;

namespace Helpdesk.Mvc.Models
{
	public class BaseEntity
    {

        public BaseEntity()
        {
            this.CreateAt = DateTime.UtcNow;
        }
        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
    }


}


