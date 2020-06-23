using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoS.Controllers
{
    public class RelatoriosController : BaseController
    {
        
       
        public IActionResult Index()
        {

            return View();
        }
    }
}