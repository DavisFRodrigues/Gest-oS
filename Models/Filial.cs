using GestaoS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoS.Models
{
    [Table("Filial")]
    public class Filial
    {
        [Display(Name = "Código")]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        public string Nome { get; set; }

        [Display(Name = "Cep")]
        [Column("Cep")]
        public string Cep { get; set; }

        [Display(Name = "Endereço")]
        [Column("Endereco")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        [Column("Numero")]
        public int Numero { get; set; }

        [Display(Name = "Bairro")]
        [Column("Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        [Column("Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "UF")]
        [Column("UF")]
        public string UF { get; set; }

        [Display(Name = "Telefone")]
        [Column("Telefone")]
        public string Telefone { get; set; }

    }

}
