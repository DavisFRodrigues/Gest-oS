using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoS.Data;
using GestaoS.Models;

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



            return View(await _context.Filial.ToListAsync());
        }

        // GET: Filials/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(filial);
        }

        // GET: Filials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
