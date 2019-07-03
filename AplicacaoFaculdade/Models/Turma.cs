using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models {
    public class Turma {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public int? Max { get; set; }
        public int? FkServico { get; set; }
        public int? FkFuncionario { get; set; }
        public bool Status { get; set; }
    }
}