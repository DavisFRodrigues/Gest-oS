﻿using Dapper;
using GestaoS.Data;
using GestaoS.Models;
using GestaoS.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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


            return View(listaLocacaoSala);
        
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
            ViewData["IdFilial"] = new SelectList(_context.Set<Filial>(), "Id", "Nome").ToList();
            ViewData["IdSala"] = new SelectList(_context.Set<Sala>(), "Id", "Nome").ToList();
            return View();
        }

        // POST: LocaSalas/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create([Bind("Id,IdFilial,IdSala,DtInicio,DtFim,UserId,TelResponsavel,Setor")] LocaSala locaSala)
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



            ViewBag.IdFilial = new SelectList(_context.Filial.Where(p => p.Id == locaSala.IdFilial), "Id", "Nome").FirstOrDefault().Text;
            ViewBag.IdSala = new SelectList(_context.Sala.Where(p => p.Id == locaSala.IdSala), "Id", "Nome").FirstOrDefault().Text;
            ViewBag.UserId = new SelectList(_context.Set<ApplicationUser>(), "Id", "NomeCompleto", locaSala.UserId).FirstOrDefault().Value;
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
                                                  WHERE Id = @id and UserId = @IdUsuarioP", new { IdUsuarioP = IdUsuario, ID = id }).ToList();

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
                var NomeFilial = NomeFilialQr[0];

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
        public JsonResult DataLocacaoSalaEdit(string DataLocacao, int idLocacaoEdit)
        {


            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                var IdSalaL = idLocacaoEdit;
                
                var ano = DataLocacao.Substring(6);

                var dia = DataLocacao.Substring(0,2);

                var mes = DataLocacao.Substring(3, 2);

                var DataLocacaoP = ano + "-" + mes + "-" + dia;

                var DataSlDisponivel = conn.Query(@"
                                                   select DATEADD(DAY,1,INICIO.DataFim) AS DATA_DISPONIVEL,INICIO.IdSala from LocacaoSala AS INICIO
                                                   where Id = @IdSala
                                                   and not EXISTS(select * from LocacaoSala where IdSala = INICIO.IdSala  and cast(@Data as date) not BETWEEN cast(DataInicio as date) AND cast(DataFim as date))", new { IdSala = IdSalaL, Data = DataLocacaoP }).ToList();

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

        [HttpGet]
        public JsonResult ObterListaLocacaoSalaEdit(int idLocacaoEdit)
        {

            var conn = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;User ID=DAVIS;Initial Catalog=GestaoS;Data Source=DAVIS-RODRIGUES");
            using (conn)
            {
                conn.Open();

                var IdLocacaoEd = idLocacaoEdit;

                var SalaOnload = conn.Query(@"  SELECT LISTA_SALA.IdFilial,LISTA_SALA.ID,LISTA_SALA.Nome from (
                                                SELECT sala.IdFilial,sala.Id,Nome,LocacaoSala.IdSala as SalaLocacao, 
                                                CASE WHEN sala.Id = LocacaoSala.IdSala THEN 1 ELSE 0 END AS COMPARACAO    
                                                FROM Sala
                                                INNER JOIN LocacaoSala on LocacaoSala.IdFilial = sala.IdFilial
                                                WHERE LocacaoSala.Id = @idLoca) AS LISTA_SALA
                                                ORDER BY	LISTA_SALA.COMPARACAO DESC
                                                ", new { idLoca = IdLocacaoEd }).ToList();

                conn.Close();

                return Json(SalaOnload);

            }



        }



    }
}
