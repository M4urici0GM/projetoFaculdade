using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Cargo {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public float Salario { get; set; }
        public bool?  Status { get; set; }
    }
}