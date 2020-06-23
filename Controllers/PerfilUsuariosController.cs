using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoS.Data;
using GestaoS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Dapper;

namespace GestaoS.Controllers
{
    public class PerfilUsuariosController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public PerfilUsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PerfilUsuarios
        public async Task<IActionResult> Index()
        {
            var temAcesso = await Usuario_Tem_Acesso("Tela Perfil de Usuário", _context);

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

            var applicationDbContext = _context.PerfilUsuario.Include(p => p.ApplicationUser).Include(p => p.TipoUsuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PerfilUsuarios/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var perfilUsuario = await _context.PerfilUsuario
        //        .Include(p => p.ApplicationUser)
        //        .Include(p => p.TipoUsuario)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (perfilUsuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView(perfilUsuario);
        //}

        // GET: PerfilUsuarios/Create
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


                ViewData["ListaSemPerfil"] = ListaSemPerfilQr;

                var Valor = ListaSemPerfilQr.Count();

                var Qtde = Valor.ToString();

                var QtdeUsuario = int.Parse(Qtde);


                TempData["QtdeSemPerfil"] = QtdeUsuario;


            }



            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto");
            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "NomeTipoUsuario");
            return View();
        }

        // POST: PerfilUsuarios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTipoUsuario,UserId")] PerfilUsuario perfilUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfilUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto", perfilUsuario.UserId);
            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "NomeTipoUsuario", perfilUsuario.IdTipoUsuario);
            return View(perfilUsuario);
        }

        // GET: PerfilUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilUsuario = await _context.PerfilUsuario.FindAsync(id);
            if (perfilUsuario == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto", perfilUsuario.UserId);
            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "NomeTipoUsuario", perfilUsuario.IdTipoUsuario);
            return PartialView(perfilUsuario);
        }

        // POST: PerfilUsuarios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTipoUsuario,UserId")] PerfilUsuario perfilUsuario)
        {
            if (id != perfilUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilUsuarioExists(perfilUsuario.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto", perfilUsuario.UserId);
            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "NomeTipoUsuario", perfilUsuario.IdTipoUsuario);
            return PartialView(perfilUsuario);
        }

        // GET: PerfilUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilUsuario = await _context.PerfilUsuario
                .Include(p => p.ApplicationUser)
                .Include(p => p.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilUsuario == null)
            {
                return NotFound();
            }

            return PartialView(perfilUsuario);
        }

        // POST: PerfilUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perfilUsuario = await _context.PerfilUsuario.FindAsync(id);
            _context.PerfilUsuario.Remove(perfilUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilUsuarioExists(int id)
        {
            return _context.PerfilUsuario.Any(e => e.Id == id);
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
//    public class PerfilUsuariosController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public PerfilUsuariosController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: PerfilUsuarios
//        public async Task<IActionResult> Index()
//        {
//            var applicationDbContext = _context.PerfilUsuario.Include(p => p.ApplicationUser).Include(p => p.TipoUsuario);
//            return View(await applicationDbContext.ToListAsync());
//        }

//        // GET: PerfilUsuarios/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var perfilUsuario = await _context.PerfilUsuario
//                .Include(p => p.ApplicationUser)
//                .Include(p => p.TipoUsuario)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (perfilUsuario == null)
//            {
//                return NotFound();
//            }

//            return View(perfilUsuario);
//        }

//        // GET: PerfilUsuarios/Create
//        public IActionResult Create()
//        {
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
//            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "Id");
//            return View();
//        }

//        // POST: PerfilUsuarios/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,IdTipoUsuario,UserId")] PerfilUsuario perfilUsuario)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(perfilUsuario);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", perfilUsuario.UserId);
//            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "Id", perfilUsuario.IdTipoUsuario);
//            return View(perfilUsuario);
//        }

//        // GET: PerfilUsuarios/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var perfilUsuario = await _context.PerfilUsuario.FindAsync(id);
//            if (perfilUsuario == null)
//            {
//                return NotFound();
//            }
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", perfilUsuario.UserId);
//            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "Id", perfilUsuario.IdTipoUsuario);
//            return View(perfilUsuario);
//        }

//        // POST: PerfilUsuarios/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTipoUsuario,UserId")] PerfilUsuario perfilUsuario)
//        {
//            if (id != perfilUsuario.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(perfilUsuario);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!PerfilUsuarioExists(perfilUsuario.Id))
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
//            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", perfilUsuario.UserId);
//            ViewData["IdTipoUsuario"] = new SelectList(_context.Set<TipoUsuario>(), "Id", "Id", perfilUsuario.IdTipoUsuario);
//            return View(perfilUsuario);
//        }

//        // GET: PerfilUsuarios/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var perfilUsuario = await _context.PerfilUsuario
//                .Include(p => p.ApplicationUser)
//                .Include(p => p.TipoUsuario)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (perfilUsuario == null)
//            {
//                return NotFound();
//            }

//            return View(perfilUsuario);
//        }

//        // POST: PerfilUsuarios/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var perfilUsuario = await _context.PerfilUsuario.FindAsync(id);
//            _context.PerfilUsuario.Remove(perfilUsuario);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool PerfilUsuarioExists(int id)
//        {
//            return _context.PerfilUsuario.Any(e => e.Id == id);
//        }
//    }
//}
