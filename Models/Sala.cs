using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Models
{

    [Table("Sala")]
    public class Sala
    {
        [Display(Name = "Código")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        public string Nome { get; set; }

        [Display(Name = "Capacidade")]
        [Column("Capacidade")]
        public int Capacidade { get; set; }

        [Display(Name = "IdFilial")]
        [ForeignKey("IdFilial")]
        

        [Column(Order = 1)]
        public int IdFilial { get; set; }
        public virtual Filial Filial { get; set; }

        [Display(Name = "Multimidia")]
        [Column("Multimidia")]
        public Boolean Multimidia { get; set; }

        [Display(Name = "Wi-fi")]
        [Column("Wifi")]
        public Boolean Wifi { get; set; }

    }
}
