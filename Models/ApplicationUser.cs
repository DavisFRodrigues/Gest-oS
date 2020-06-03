﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NomeCompleto { get; set; }
    }
}