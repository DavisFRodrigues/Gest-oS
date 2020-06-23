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


            return View(listaLocacaoEquipamento);
        }

        // GET: LocaEquipamentoes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var locaEquipamento = await _context.LocaEquipamento
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (locaEquipamento == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView(locaEquipamento);
        //}

        // GET: LocaEquipamentoes/Create
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


            ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto").ToList();
            ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome");
            ViewData["IdEquipamento"] = new SelectList(_context.Set<Equipamento>().ToList(), "Id", "Nome");
            return View();
        }

        // POST: LocaEquipamentoes/Create

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

                    var idPerfil = ObterPerfil();


                    var idAlteracao = ObterLocacaoPorUsuario(id);

                    if (idPerfil == 1 && idAlteracao == 1)
                    {
                        _context.Update(locaEquipamento);
                        await _context.SaveChangesAsync();


                    }
                    else if (idPerfil != 1)
                    {
                        _context.Update(locaEquipamento);
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

            var idPerfil = ObterPerfil();


            var idAlteracao = ObterLocacaoPorUsuario(id);

            if (idPerfil == 1 && idAlteracao == 1)
            {
                var locaEquipamento = await _context.LocaEquipamento.FindAsync(id);
                _context.LocaEquipamento.Remove(locaEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else if (idPerfil != 1)
            {
                var locaEquipamento = await _context.LocaEquipamento.FindAsync(id);
                _context.LocaEquipamento.Remove(locaEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else
            {

                TempData["msg"] = "Você não tem permissão para alterar registro de outros usuários.";

                return RedirectToAction("Index");

            }


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

        public JsonResult QtdeEquipamentoEdit(int IdEquipamentoQtdeEdit)
        {

            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                var IdLoca = IdEquipamentoQtdeEdit;

                var EqQtdeDisponivel = conn.Query(@"SELECT Equipamento.Quantidade-(sum(LocEquiQtde)) AS QTDE_DISPONIVEL,LocacaoEquipamento.IdEquipamento,LocacaoEquipamento.LocEquiQtde 
                                                    FROM Equipamento (NOLOCK)
                                                    LEFT JOIN LocacaoEquipamento ON Equipamento.Id = LocacaoEquipamento.IdEquipamento 
                                                    Where LocacaoEquipamento.Id = @IdEquipamento 
                                                    GROUP by Equipamento.Quantidade,LocacaoEquipamento.IdEquipamento,LocacaoEquipamento.LocEquiQtde
                                                    ", new { IdEquipamento = IdLoca }).ToList();

                conn.Close();

                return Json(EqQtdeDisponivel);

            }



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
                                                  WHERE Id = @id and UserId = @IdUsuarioP", new { IdUsuarioP = IdUsuario, ID = id }).ToList();

                conn.Close();
                var IdLocacaoSalaUSer = IdLocacaoSalaU.Count();
                var IdLocacaoSala = IdLocacaoSalaUSer.ToString();
                return int.Parse(IdLocacaoSala);

            }

        }

        [HttpGet]
        public JsonResult ObterListaLocacaoEdit(int idLocacaoEdit)
        {

            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                var IdLocacaoEd = idLocacaoEdit;

                var SalaOnload = conn.Query(@"SELECT LISTA_SALA.IdFilial,LISTA_SALA.ID,LISTA_SALA.Nome from (
  SELECT sala.IdFilial,sala.Id,Nome,LocacaoEquipamento.IdSala as SalaLocacao, 
  CASE WHEN sala.Id = LocacaoEquipamento.IdSala THEN 1 ELSE 0 END AS COMPARACAO    
  FROM Sala
  INNER JOIN LocacaoEquipamento on LocacaoEquipamento.IdFilial = sala.IdFilial
  WHERE LocacaoEquipamento.Id = @idLoca) AS LISTA_SALA
  ORDER BY	LISTA_SALA.COMPARACAO DESC
  ", new { idLoca = IdLocacaoEd }).ToList();

                conn.Close();

                return Json(SalaOnload);

            }



        }


    }
}
