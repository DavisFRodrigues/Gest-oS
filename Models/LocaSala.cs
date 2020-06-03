using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GestaoS.Models
{
    [Table("LocacaoSala")]
    public class LocaSala
    {
        [Display(Name = "Código")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "IdFilial")]
        [ForeignKey("IdFilial")]
        [Column(Order = 1)]
        public int IdFilial { get; set; }
        public virtual Filial Filial { get; set; }

        [Display(Name = "IdSala")]
        [ForeignKey("IdSala")]
        [Column(Order = 1)]
        public int IdSala { get; set; }
        public virtual Sala Sala { get; set; }

        [Display(Name = "Data Início")]
        [DataType(DataType.Date)]
        [Column("DataInicio")]
        public DateTime DtInicio { get; set; }

        [Display(Name = "Data Fim")]
        [DataType(DataType.Date)]
        [Column("DataFim")]
        public DateTime DtFim { get; set; }

        [Display(Name = "Responsável")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser   { get; set; }

        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        [Column("TelResponsavel")]
        public string TelResponsavel{ get; set; }

        [Display(Name = "Setor")]
        [Column("Setor")]
        public string Setor { get; set; }

        

    }
}
