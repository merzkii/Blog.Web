﻿using Blog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; } 
        public string Password { get; set; } 
        public UserRoles Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
