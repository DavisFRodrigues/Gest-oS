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
    public class AcessoTipoUsuariosController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AcessoTipoUsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AcessoTipoUsuarios
        public async Task<IActionResult> Index()
        {
            var temAcesso = await Usuario_Tem_Acesso("Tela Tipo de Acesso do Usuário", _context);

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

            var applicationDbContext = _context.AcessoTipoUsuario.Include(a => a.TipoUsuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AcessoTipoUsuarios/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var acessoTipoUsuario = await _context.AcessoTipoUsuario
        //        .Include(a => a.TipoUsuario)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (acessoTipoUsuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(acessoTipoUsuario);
        //}

        // GET: AcessoTipoUsuarios/Create
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


            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "NomeTipoUsuario");
            return View();
        }

        // POST: AcessoTipoUsuarios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFuncionalidade,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acessoTipoUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            return View(acessoTipoUsuario);
        }

        // GET: AcessoTipoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            return PartialView(acessoTipoUsuario);
        }

        // POST: AcessoTipoUsuarios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFuncionalidade,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
        {
            if (id != acessoTipoUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acessoTipoUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcessoTipoUsuarioExists(acessoTipoUsuario.Id))
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
            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            return PartialView(acessoTipoUsuario);
        }

        // GET: AcessoTipoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario
                .Include(a => a.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }

            return PartialView(acessoTipoUsuario);
        }

        // POST: AcessoTipoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
            _context.AcessoTipoUsuario.Remove(acessoTipoUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcessoTipoUsuarioExists(int id)
        {
            return _context.AcessoTipoUsuario.Any(e => e.Id == id);
        }
    }
}


















//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using GestaoS.Data;
//using GestaoS.Models;

//namespace GestaoS.Controllers
//{
//    public class AcessoTipoUsuariosController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public AcessoTipoUsuariosController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: AcessoTipoUsuarios
//        public async Task<IActionResult> Index()
//        {
//            var applicationDbContext = _context.AcessoTipoUsuario.Include(a => a.TipoUsuario);
//            return View(await applicationDbContext.ToListAsync());
//        }

//        // GET: AcessoTipoUsuarios/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var acessoTipoUsuario = await _context.AcessoTipoUsuario
//                .Include(a => a.TipoUsuario)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (acessoTipoUsuario == null)
//            {
//                return NotFound();
//            }

//            return View(acessoTipoUsuario);
//        }

//        // GET: AcessoTipoUsuarios/Create
//        public IActionResult Create()
//        {
//            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "Id");
//            return View();
//        }

//        // POST: AcessoTipoUsuarios/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,NomeFuncionalidade,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(acessoTipoUsuario);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "Id", acessoTipoUsuario.IdTipoUsuario);
//            return View(acessoTipoUsuario);
//        }

//        // GET: AcessoTipoUsuarios/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
//            if (acessoTipoUsuario == null)
//            {
//                return NotFound();
//            }
//            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "Id", acessoTipoUsuario.IdTipoUsuario);
//            return View(acessoTipoUsuario);
//        }

//        // POST: AcessoTipoUsuarios/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFuncionalidade,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
//        {
//            if (id != acessoTipoUsuario.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(acessoTipoUsuario);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!AcessoTipoUsuarioExists(acessoTipoUsuario.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "Id", acessoTipoUsuario.IdTipoUsuario);
//            return View(acessoTipoUsuario);
//        }

//        // GET: AcessoTipoUsuarios/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var acessoTipoUsuario = await _context.AcessoTipoUsuario
//                .Include(a => a.TipoUsuario)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (acessoTipoUsuario == null)
//            {
//                return NotFound();
//            }

//            return View(acessoTipoUsuario);
//        }

//        // POST: AcessoTipoUsuarios/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
//            _context.AcessoTipoUsuario.Remove(acessoTipoUsuario);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool AcessoTipoUsuarioExists(int id)
//        {
//            return _context.AcessoTipoUsuario.Any(e => e.Id == id);
//        }
//    }
//}
