using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Models.ViewModel
{
    public class DevolucaoViewModel
    {
        public int IdDevoLocaEquipamento { get; set; }
        public int IdLocaEquipamento { get; set; }
        public string NomeEquipamento { get; set; }

        public int NmPatrimonio { get; set; }

        [Display(Name = "Data Início")]
        [DataType(DataType.Date)]
        [Column("DataInicio")]
        public DateTime DtInicio { get; set; }

        [Display(Name = "Data Fim")]
        [DataType(DataType.Date)]
        [Column("DataFim")]
        public DateTime DtFim { get; set; }
        public string Respon { get; set; }
        public Boolean Devolvido { get; set; }
    }
}
