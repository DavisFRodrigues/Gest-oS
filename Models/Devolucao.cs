using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Models
{ 
[Table("Devolucao")]
public class Devolucao
{

    
    public int Id { get; set; }

       public int IdLocaEquipamento { get; set; }
       public string NomeEquipamento { get; set; }
   
        public int NmPatrimonio { get; set; }

    [Display(Name = "Data Início")]
    [DataType(DataType.Date)]
    [Column("DataInicio")]
    public DateTime DataInicio { get; set; }

    [Display(Name = "Data Fim")]
    [DataType(DataType.Date)]
    [Column("DataFim")]
    public DateTime DataFim { get; set; }

    public string NomeCompleto { get; set; }
   
    public Boolean Devolvido { get; set; }

}
}
