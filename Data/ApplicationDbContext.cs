using System;
using System.Collections.Generic;
using System.Text;
using GestaoS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<GestaoS.Models.AcessoTipoUsuario> AcessoTipoUsuario { get; set; }

        public DbSet<GestaoS.Models.PerfilUsuario> PerfilUsuario { get; set; }

        public DbSet<GestaoS.Models.TipoUsuario> TipoUsuario { get; set; }

        public DbSet<GestaoS.Models.Filial> Filial { get; set; }

        public DbSet<GestaoS.Models.Sala> Sala { get; set; }

        public DbSet<GestaoS.Models.Equipamento> Equipamento { get; set; }

        public DbSet<GestaoS.Models.LocaEquipamento> LocaEquipamento { get; set; }

        public DbSet<GestaoS.Models.LocaSala> LocaSala { get; set; }
    }
}
