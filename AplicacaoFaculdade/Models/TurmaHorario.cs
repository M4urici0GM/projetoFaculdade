using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models {
    public class TurmaHorario {
        public int? Id { get; set; }
        public int FkTurma { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public int Dia { get; set; }
        public bool Status { get; set; }
    }
}