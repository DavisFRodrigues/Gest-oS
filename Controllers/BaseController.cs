using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GestaoS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GestaoS.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public async Task<bool> Usuario_Tem_Acesso(string NomePagina, Data.ApplicationDbContext _context)
        {

            var usuario = User.Identity.Name;


            var temAcesso = await (from TP in _context.TipoUsuario
                                   join AT in _context.AcessoTipoUsuario on TP.Id equals AT.IdTipoUsuario
                                   join PF in _context.PerfilUsuario on TP.Id equals PF.IdTipoUsuario
                                   join US in _context.Users on PF.UserId equals US.Id
                                   where AT.NomeFuncionalidade == NomePagina && US.Email == usuario
                                   select new
                                   {
                                       TP.Id
                                   }).AnyAsync();


            return temAcesso;

        }
               
    }
}