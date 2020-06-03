using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Models.ViewModel
{
    public class LocaSalaViewModel
    {
        public int IdLocaSala { get; set; }
        public string IdFilial { get; set; }
        public string IdSala { get; set; }
        
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
        public string SetorLocaSala { get; set; }

    }
}
