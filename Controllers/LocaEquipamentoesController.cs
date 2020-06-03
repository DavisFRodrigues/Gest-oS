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
using System.Configuration;
using GestaoS.Models.ViewModel;

namespace GestaoS.Controllers
{
    public class LocaEquipamentoesController : BaseController
    {

        private readonly ApplicationDbContext _context;

        public LocaEquipamentoesController(ApplicationDbContext context)
        {
            _context = context;
        }

            



        // GET: LocaEquipamentoes
        public IActionResult Index()
        {

            //var temAcesso = await Usuario_Tem_Acesso("Tela Locação Equipamento", _context);

            //if (!temAcesso)
            //{
            //    return RedirectToAction("Index", "Home");
            //}


            var applicationDbContext = _context.LocaEquipamento.Include(p => p.Filial).Include(p => p.Sala).Include(p => p.Equipamento).Include(p => p.ApplicationUser);

            var listaEquipamento = (from LCEQ in _context.LocaEquipamento
                             join FL in _context.Filial on LCEQ.IdFilial equals FL.Id
                             join SL in _context.Sala on LCEQ.IdSala equals SL.Id
                             join EQ in _context.Equipamento on LCEQ.IdEquipamento equals EQ.Id
                             join APUS in _context.Users on LCEQ.UserId equals APUS.Id
                             select
                             new
                             {
                                 LCEQ.Id,
                                 NomeFilial = FL.Nome,
                                 NomeSala = SL.Nome,
                                 NomeEquipamento = EQ.Nome,
                                 LCEQ.Qtde,
                                 LCEQ.DtInicio,
                                 LCEQ.DtFim,
                                 APUS.NomeCompleto,
                                 LCEQ.TelResponsavel,
                                 LCEQ.Setor

                             }).ToList();

            List<LocaEquipamentoViewModel> listaLocacaoEquipamento = new List<LocaEquipamentoViewModel>();

            foreach (var item in listaEquipamento)
            {
                LocaEquipamentoViewModel locaEquipamentoVM = new LocaEquipamentoViewModel();
                locaEquipamentoVM.IdLocacao = item.Id;
                locaEquipamentoVM.IdFilial = item.NomeFilial;
                locaEquipamentoVM.IdSala = item.NomeSala;
                locaEquipamentoVM.IdEquipamento = item.NomeEquipamento;
                locaEquipamentoVM.QtdeEqLocado = item.Qtde;
                locaEquipamentoVM.Datainicio = item.DtInicio;
                locaEquipamentoVM.DataFim = item.DtFim;
                locaEquipamentoVM.Resp = item.NomeCompleto;
                locaEquipamentoVM.TelResp = item.TelResponsavel;
                locaEquipamentoVM.SetorLoca = item.Setor;

                listaLocacaoEquipamento.Add(locaEquipamentoVM);


            }




            return View(listaLocacaoEquipamento) ;
        }

        // GET: LocaEquipamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locaEquipamento = await _context.LocaEquipamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locaEquipamento == null)
            {
                return NotFound();
            }

            return PartialView(locaEquipamento);
        }

        // GET: LocaEquipamentoes/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto").ToList();
            ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome");
            ViewData["IdEquipamento"] = new SelectList(_context.Set<Equipamento>().ToList(), "Id", "Nome");
            return View();
        }

        // POST: LocaEquipamentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdFilial,IdSala,IdEquipamento,Qtde,DtInicio,DtFim,UserId,TelResponsavel,Setor")] LocaEquipamento locaEquipamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locaEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(locaEquipamento);
        }

        // GET: LocaEquipamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locaEquipamento = await _context.LocaEquipamento.FindAsync(id);
            if (locaEquipamento == null)
            {
                return NotFound();
            }

            ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome", locaEquipamento.IdFilial).ToList();
            ViewData["IdSala"] = new SelectList(_context.Set<Sala>(), "Id", "Nome", locaEquipamento.IdSala).ToList();
            ViewData["IdEquipamento"] = new SelectList(_context.Set<Equipamento>().ToList(), "Id", "Nome", locaEquipamento.IdEquipamento).ToList();
            ViewBag.UserId = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto", locaEquipamento.UserId).ToList();
            return PartialView(locaEquipamento);
        }

        ////// POST: LocaEquipamentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdFilial,IdSala,IdEquipamento,Qtde,DtInicio,DtFim,UserId,TelResponsavel,Setor")] LocaEquipamento locaEquipamento)
        {
            if (id != locaEquipamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    
                    _context.Update(locaEquipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocaEquipamentoExists(locaEquipamento.Id))
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

           
            return View(locaEquipamento);
        }

        // GET: LocaEquipamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locaEquipamento = await _context.LocaEquipamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locaEquipamento == null)
            {
                return NotFound();
            }

             ViewBag.IdFilial = new SelectList(_context.Filial.Where(P => P.Id == locaEquipamento.IdFilial), "Id", "Nome").FirstOrDefault().Text;
             ViewBag.IdSala = new SelectList(_context.Sala.Where(P => P.Id == locaEquipamento.IdSala), "Id", "Nome").FirstOrDefault().Text;
             ViewBag.IdEquipamento = new SelectList(_context.Equipamento.Where(P => P.Id == locaEquipamento.IdEquipamento), "Id", "Nome").FirstOrDefault().Text;
             ViewBag.UserId = new SelectList(_context.Users.Where(p => p.Id == locaEquipamento.UserId), "Id", "NomeCompleto", locaEquipamento.UserId).FirstOrDefault().Text;
            return PartialView(locaEquipamento);
        }

        // POST: LocaEquipamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locaEquipamento = await _context.LocaEquipamento.FindAsync(id);
            _context.LocaEquipamento.Remove(locaEquipamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocaEquipamentoExists(int id)
        {
            return _context.LocaEquipamento.Any(e => e.Id == id);
        }



        [HttpGet]
        public JsonResult ObterSala(int idFilial)
        {
            var ListSala = from SL in _context.Sala
                           where SL.IdFilial == idFilial
                           select
                           new
                           {
                               SL.Id,
                               SL.Nome
                           };
            var BuscaSala = ListSala.ToList();

            return Json(BuscaSala);

        }

        [HttpGet]
        public JsonResult QtdeEquipamento(int idEquipamentoQtde)
        {

            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                var IdEquipamentoP = idEquipamentoQtde;

                var EqQtdeDisponivel = conn.Query(@"SELECT Equipamento.Quantidade-(sum(LocEquiQtde)) AS QTDE_DISPONIVEL 
                                                    FROM Equipamento JOIN LocacaoEquipamento ON 
                                                    Equipamento.Id = LocacaoEquipamento.IdEquipamento 
                                                    Where LocacaoEquipamento.IdEquipamento = @IdEquipamento 
                                                    GROUP by Equipamento.Quantidade", new { IdEquipamento = IdEquipamentoP }).ToList();

                conn.Close();

                return Json(EqQtdeDisponivel);

            }



        }



    }
}
