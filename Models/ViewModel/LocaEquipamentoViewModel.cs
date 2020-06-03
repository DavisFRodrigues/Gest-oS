using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Models.ViewModel
{
    public class LocaEquipamentoViewModel
    {
        public int IdLocacao { get; set; }
        public string IdFilial { get; set; }
        public string IdEquipamento { get; set; }
        public string IdSala { get; set; }
        public int QtdeEqLocado { get; set; }

        [Display(Name = "Data Início")]
        [DataType(DataType.Date)]
        [Column("DataInicio")]
        public DateTime Datainicio { get; set; }

        [Display(Name = "Data Fim")]
        [DataType(DataType.Date)]
        [Column("DataFim")]
        public DateTime DataFim { get; set; }
        public string Resp { get; set; }
        public string TelResp { get; set; }
        public string SetorLoca { get; set; }

        //public LocaEquipamento LocaEquipamento { get; }

    }
}
