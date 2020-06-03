using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Models.ViewModel
{
    public class SalaViewModel
    {
        public int IdSala { get; set; }
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public string IdFilial{ get; set; }
        public Boolean Multimidia { get; set; }
        public Boolean Wifi { get; set; }
    }
}
