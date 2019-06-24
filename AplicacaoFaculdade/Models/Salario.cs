using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models {
    public class Salario {

        
        public int? Id { get; set; }
        public float Valor { get; set; }
        public DateTime? dataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public bool Status { get; set; }
        public int? FkCargo { get; set; }

    }
}