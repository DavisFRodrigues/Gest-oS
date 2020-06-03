using Dapper;
using GestaoS.Data;
using GestaoS.Models;
using GestaoS.Models.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Controllers
{
    public class LocaSalasController : BaseController
    {
        private readonly ApplicationDbContext _context;


        public LocaSalasController(ApplicationDbContext context)
        {
            _context = context;


        }


        //// GET: LocaSalas
        public IActionResult Index()
        {

            ////var temAcesso = await Usuario_Tem_Acesso("Tela Locação Sala", _context);

            ////if (!temAcesso)
            ////{
            ////    return RedirectToAction("Index", "Home");
            ////}

            var applicationDbContext = _context.LocaSala.Include(p => p.Filial).Include(p => p.Sala).Include(p => p.ApplicationUser);

            var listaSala = (from LCSL in _context.LocaSala
                                    join FL in _context.Filial on LCSL.IdFilial equals FL.Id
                                    join SL in _context.Sala on LCSL.IdSala equals SL.Id
                                    join APUS in _context.Users on LCSL.UserId equals APUS.Id
                                    select
                                    new
                                    {
                                        LCSL.Id,
                                        NomeFilial = FL.Nome,
                                        NomeSala = SL.Nome,
                                        LCSL.DtInicio,
                                        LCSL.DtFim,
                                        APUS.NomeCompleto,
                                        LCSL.TelResponsavel,
                                        LCSL.Setor

                                    }).ToList();

            List<LocaSalaViewModel> listaLocacaoSala = new List<LocaSalaViewModel>();

            foreach (var item in listaSala)
            {
                LocaSalaViewModel locaSalaVM = new LocaSalaViewModel();
                locaSalaVM.IdLocaSala = item.Id;
                locaSalaVM.IdFilial = item.NomeFilial;
                locaSalaVM.IdSala = item.NomeSala;
                locaSalaVM.Datainicio = item.DtInicio;
                locaSalaVM.DataFim = item.DtFim;
                locaSalaVM.Resp = item.NomeCompleto;
                locaSalaVM.TelResp = item.TelResponsavel;
                locaSalaVM.SetorLocaSala = item.Setor;

                listaLocacaoSala.Add(locaSalaVM);


            }













            //ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto").FirstOrDefault().Text;
            //ViewBag.IdFilial = new SelectList(_context.Set<Filial>(), "Id", "Nome").FirstOrDefault().Text;
            //ViewBag.IdSala = new SelectList(_context.Set<Sala>(), "Id", "Nome").FirstOrDefault().Text;

            return View(listaLocacaoSala);// await _context.LocaSala.ToListAsync());
        }

        // GET: LocaSalas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locaSala = await _context.LocaSala
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locaSala == null)
            {
                return NotFound();
            }

            return View(locaSala);
        }

        // GET: LocaSalas/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto").ToList();
            ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome").ToList();
            ViewData["IdSala"] = new SelectList(_context.Set<Sala>(), "Id", "Nome").ToList();
            return View();
        }

        // POST: LocaSalas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdFilial,IdSala,DtInicio,DtFim,UserId,TelResponsavel,Setor")] LocaSala locaSala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locaSala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(locaSala);
        }

        // GET: LocaSalas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locaSala = await _context.LocaSala.FindAsync(id);
            if (locaSala == null)
            {
                return NotFound();
            }
            
            ViewBag.IdFilial = new SelectList(_context.Set<Filial>(), "Id", "Nome", locaSala.IdFilial);
            ViewBag.IdSala = new SelectList(_context.Set<Sala>(), "Id", "Nome", locaSala.IdSala);
            ViewBag.UserId = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto", locaSala.UserId);
            return PartialView(locaSala);
        }

        // POST: LocaSalas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdFilial,IdSala,DtInicio,DtFim,UserId,TelResponsavel,Setor")] LocaSala locaSala)
        {
            if (id != locaSala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //var userid = User.Claims.First().Value;


                    
                    var idPerfil = ObterPerfil();

                    
                    var idAlteracao = ObterLocacaoPorUsuario(id);

                    if (idPerfil == 1 && idAlteracao == 1)
                    {
                        _context.Update(locaSala);
                        await _context.SaveChangesAsync();
                    }
                    else if (idPerfil != 1)
                    {
                        _context.Update(locaSala);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        
                        TempData["msg"] = "Você não tem permissão para alterar registro de outros usuários.";

                        return RedirectToAction("Index");

                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocaSalaExists(locaSala.Id))
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
            return View(locaSala);
        }

        // GET: LocaSalas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locaSala = await _context.LocaSala
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locaSala == null)
            {
                return NotFound();
            }

            

            ViewBag.IdFilial = new SelectList(_context.Filial.Where(p=> p.Id == locaSala.IdFilial), "Id", "Nome").FirstOrDefault().Text;
            ViewBag.IdSala = new SelectList(_context.Sala.Where(p => p.Id == locaSala.IdSala), "Id", "Nome").FirstOrDefault().Text;
            ViewBag.UserId = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto",locaSala.UserId).FirstOrDefault().Value;
            return PartialView(locaSala);






        }

        // POST: LocaSalas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var idPerfil = ObterPerfil();


            var idAlteracao = ObterLocacaoPorUsuario(id);

            if (idPerfil == 1 && idAlteracao == 1)
            {
                var locaSala = await _context.LocaSala.FindAsync(id);
                _context.LocaSala.Remove(locaSala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (idPerfil != 1)
            {
                var locaSala = await _context.LocaSala.FindAsync(id);
                _context.LocaSala.Remove(locaSala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {

                TempData["msg"] = "Você não tem permissão para alterar registro de outros usuários.";

                return RedirectToAction("Index");

            }





          
        }

        private bool LocaSalaExists(int id)
        {
            return _context.LocaSala.Any(e => e.Id == id);
        }


        public int ObterPerfil()
        {


            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                var IdUsuario = User.Claims.First().Value;

                var PerfilUsu = conn.Query(@"SELECT ISNULL(IdTipoUsuario,0) AS TIPO FROM  PerfilUsuario WHERE IdTipoUsuario = 3 AND UserId =@IdUsuarioP", new { IdUsuarioP = IdUsuario }).ToList();

                conn.Close();
                var PerfilUsuarioP = PerfilUsu.Count();
                var PerfilUsuario = PerfilUsuarioP.ToString();
                return int.Parse(PerfilUsuario);

            }
 
        }



        public int ObterLocacaoPorUsuario(int id)
        {


            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                var IdUsuario = User.Claims.First().Value;

                var IdLocacaoSalaU = conn.Query(@"SELECT ISNULL(Id,0) AS ID FROM LocacaoSala
                                                  WHERE Id = @id and UserId = @IdUsuarioP", new { IdUsuarioP = IdUsuario,ID = id }).ToList();

                conn.Close();
                var IdLocacaoSalaUSer = IdLocacaoSalaU.Count();
                var IdLocacaoSala = IdLocacaoSalaUSer.ToString();
                return int.Parse(IdLocacaoSala);

            }

        }



        public int ObterNomeFilial(string id)
        {


            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                

                var NomeFilialQr = conn.Query(@"SELECT Filial.Nome FROM 
                                                  LocacaoSala (NOLOCK)
                                                  INNER JOIN Filial ON Filial.Id = IdFilial 
                                                  WHERE LocacaoSala.Id = @ID", new { ID = id }).FirstOrDefault().Nome;
                                                  
                conn.Close();
                var NomeFilial =  NomeFilialQr[0];
                
                return int.Parse(NomeFilial);

            }

        }






        [HttpGet]
        public JsonResult DataLocacaoSala(string DataLocacao, int idSalaData)
        {


            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                var IdSalaL = idSalaData;
                var DataLocacaoP = DataLocacao;

                var DataSlDisponivel = conn.Query(@"SELECT DATEADD(DAY,1,DataFim) AS DATA_DISPONIVEL FROM LocacaoSala
                 WHERE @Data BETWEEN DataInicio AND DataFim AND  LocacaoSala.IdSala = @IdSala", new { IdSala = IdSalaL, Data = DataLocacaoP }).ToList();

                conn.Close();

                return Json(DataSlDisponivel);

            }



        }



        [HttpGet]
        public JsonResult DataLocacaoSalaFim(string DtFimLocacaoS, int idSalaData)
        {


            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                var IdSalaL = idSalaData;
                var DataLocacaoP = DtFimLocacaoS;

                var DataSlDisponivel = conn.Query(@"SELECT DATEADD(DAY,1,DataFim) AS DATA_DISPONIVEL FROM LocacaoSala
                 WHERE @Data BETWEEN DataInicio AND DataFim AND  LocacaoSala.IdSala = @IdSala", new { IdSala = IdSalaL, Data = DataLocacaoP }).ToList();

                conn.Close();

                return Json(DataSlDisponivel);

            }



        }

        
       


    }
}
