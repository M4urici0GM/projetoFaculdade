using System;
namespace AplicacaoFaculdade.Models {
    public class Contrato {
        public int? Id { get; set; }
        public int? FkAluno { get; set; }
        public DateTime Data { get; set; }
        public int? Vencimento { get; set; }
        public bool? Ativo { get; set; }
    }
}
