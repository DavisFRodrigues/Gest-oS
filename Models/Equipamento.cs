using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Models
{
    [Table("Equipamento")]
    public class Equipamento
    {
        [Display(Name = "Código")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Column("Descricao")]
        public string Descricao { get; set; }

        [Display(Name = "Número de Patrimonio")]
        [Column("NmPatrimonio")]
        public int NmPatrimonio { get; set; }

        [Display(Name = "Quantidade")]
        [Column("Quantidade")]
        public int Quantidade { get; set; }

    }
}
