using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoS.Data;
using GestaoS.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace GestaoS.Controllers
{
    public class FilialsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public FilialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Filials
        public async Task<IActionResult> Index()
        {
            var temAcesso = await Usuario_Tem_Acesso("Tela de Filial", _context);

            if (!temAcesso)
            {
                return RedirectToAction("Index", "Home");
            }

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

            

            return View(await _context.Filial.ToListAsync());
        }

        // GET: Filials/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var filial = await _context.Filial
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (filial == null)
        //    {
        //        return NotFound();
        //    }

        //    var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
        //    using (conn)
        //    {
        //        conn.Open();



        //        var ListaSemPerfilQr = conn.Query(@"SELECT NOMECOMPLETO FROM ASPNETUSERS (NOLOCK)
        //                                            WHERE NOT EXISTS(SELECT USERID FROM PerfilUsuario(NOLOCK) WHERE AspNetUsers.Id = UserId)
        //                                            ORDER BY NomeCompleto").ToList();

        //        conn.Close();

        //        var Valor = ListaSemPerfilQr.Count();

        //        var Qtde = Valor.ToString();

        //        var QtdeUsuario = int.Parse(Qtde);


        //        TempData["QtdeSemPerfil"] = QtdeUsuario;


        //    }

        //    return View(filial);
        //}

        // GET: Filials/Create
        public IActionResult Create()
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

        // POST: Filials/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cep,Endereco,Numero,Bairro,Cidade,UF,Telefone")] Filial filial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            TempData["QtdeSemPerfil"] = TempData["QtdeSemPerfilV"];
            return View(filial);
        }

        // GET: Filials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filial = await _context.Filial.FindAsync(id);
            if (filial == null)
            {
                return NotFound();
            }

            return PartialView(filial);
        }

        // POST: Filials/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cep,Endereco,Numero,Bairro,Cidade,UF,Telefone")] Filial filial)
        {
            if (id != filial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilialExists(filial.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(filial);
        }

        // GET: Filials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filial = await _context.Filial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filial == null)
            {
                return NotFound();
            }
            
            return PartialView(filial);
        }

        // POST: Filials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filial = await _context.Filial.FindAsync(id);
            _context.Filial.Remove(filial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilialExists(int id)
        {
            return _context.Filial.Any(e => e.Id == id);
        }
    }
}
