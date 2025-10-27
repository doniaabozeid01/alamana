﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace alamana.Infrastructure.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
    }
}
