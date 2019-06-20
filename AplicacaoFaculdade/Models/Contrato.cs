using System;
namespace AplicacaoFaculdade.Models {
    public class Contrato {
        public int contratoId { get; set; }
        public int contratoFkAluno { get; set; }
        public DateTime contratoData { get; set; }
        public DateTime contratoVencimento { get; set; }
        public bool contratoAtivo { get; set; }
    }
}
