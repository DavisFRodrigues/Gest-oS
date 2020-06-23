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
    public class DevolucaosController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public DevolucaosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Devolucaos
        public async Task<IActionResult> Index()
        {
            var temAcesso = await Usuario_Tem_Acesso("Tela de Devolucao", _context);

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

            return View(await _context.Devolucao.ToListAsync());
        }

        // GET: Devolucaos/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var devolucao = await _context.Devolucao
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (devolucao == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(devolucao);
        //}

        // GET: Devolucaos/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Devolucaos/Create
       
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,IdLocaEquipamento,NomeEquipamento,NmPatrimonio,DataInicio,DataFim,NomeCompleto,Devolvido")] Devolucao devolucao)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(devolucao);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(devolucao);
        //}

        // GET: Devolucaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devolucao = await _context.Devolucao.FindAsync(id);
            if (devolucao == null)
            {
                return NotFound();
            }
            return  PartialView(devolucao);
        }

        // POST: Devolucaos/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdLocaEquipamento,NomeEquipamento,NmPatrimonio,DataInicio,DataFim,NomeCompleto,Devolvido")] Devolucao devolucao)
        {
            if (id != devolucao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(devolucao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevolucaoExists(devolucao.Id))
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
            return View(devolucao);
        }

        // GET: Devolucaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devolucao = await _context.Devolucao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (devolucao == null)
            {
                return NotFound();
            }

            return PartialView(devolucao);
        }

        // POST: Devolucaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var devolucao = await _context.Devolucao.FindAsync(id);
            _context.Devolucao.Remove(devolucao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DevolucaoExists(int id)
        {
            return _context.Devolucao.Any(e => e.Id == id);
        }
    }
}
