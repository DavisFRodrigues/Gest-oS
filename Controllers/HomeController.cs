using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GestaoS.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace GestaoS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();



                var ListaSemPerfilQr = conn.Query(@"SELECT NOMECOMPLETO FROM ASPNETUSERS (NOLOCK)
                                                    WHERE NOT EXISTS(SELECT USERID FROM PerfilUsuario(NOLOCK) WHERE AspNetUsers.Id = UserId)
                                                    ORDER BY NomeCompleto").ToList();

                conn.Close();

                var Valor = ListaSemPerfilQr.Count();

                var Qtde = Valor.ToString();

                var QtdeUsuario = int.Parse(Qtde);


                TempData["QtdeSemPerfil"] = QtdeUsuario;


            }

            

            return View();
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

  

    }
}