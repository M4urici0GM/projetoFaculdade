using System;
namespace AplicacaoFaculdade.Models {
    public class Telefone {
        public int? Id { get; set; }
        public int Ddd { get; set; }
        public int Numero { get; set; }
        public bool? Padrao { get; set; }
        public int FkTipo { get; set; }
        public bool? Status { get; set; }
    }
}
