using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoS.Data;
using GestaoS.Models;
using GestaoS.Models.ViewModel;
using Microsoft.Data.SqlClient;
using Dapper;

namespace GestaoS.Controllers
{
    public class SalasController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public SalasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Salas1
        public async Task<IActionResult> Index()
        {

            var temAcesso = await Usuario_Tem_Acesso("Tela de Sala", _context);

            if (!temAcesso)
            {
                return RedirectToAction("Index", "Home");
            }



            var applicationDbContext = _context.Sala.Include(p => p.Filial);

            var listaSala = (from SL in _context.Sala
                             join FL in _context.Filial on SL.IdFilial equals FL.Id
                             select
                             new
                             {
                                 SL.Id,
                                 SL.Nome,
                                 SL.Capacidade,
                                 NomeFilial = FL.Nome,
                                 SL.Multimidia,
                                 SL.Wifi

                             }).ToList();

            List<SalaViewModel> listaSalaVM = new List<SalaViewModel>();

            foreach (var item in listaSala)
            {
                SalaViewModel SalaVM = new SalaViewModel();

                SalaVM.IdSala = item.Id;
                SalaVM.Nome = item.Nome;
                SalaVM.Capacidade = item.Capacidade;
                SalaVM.IdFilial = item.NomeFilial;
                SalaVM.Multimidia = item.Multimidia;
                SalaVM.Wifi = item.Wifi;

                listaSalaVM.Add(SalaVM);


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

            return View(listaSalaVM);
        }

        // GET: Salas1/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sala = await _context.Sala
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (sala == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome", sala.IdFilial);
        //    return View(sala);
        //}

        // GET: Salas1/Create
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

            ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome");
            return View();
        }

        // POST: Salas1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Capacidade,IdFilial,Multimidia,Wifi")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome", sala.IdFilial);
            return View(sala);
        }

        // GET: Salas1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome", sala.IdFilial);
            return PartialView(sala);
        }

        // POST: Salas1/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Capacidade,IdFilial,Multimidia,Wifi")] Sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.Id))
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
            ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome", sala.IdFilial);
            return View(sala);
        }

        // GET: Salas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Sala
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }
            ViewData["IdFilial"] = new SelectList(_context.Filial.Where(p => p.Id == sala.IdFilial), "Id", "Nome").FirstOrDefault().Text;

            return PartialView(sala);
        }

        // POST: Salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sala = await _context.Sala.FindAsync(id);
            _context.Sala.Remove(sala);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaExists(int id)
        {
            return _context.Sala.Any(e => e.Id == id);
        }
    }
}
