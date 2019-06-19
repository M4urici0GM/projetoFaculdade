using System;
namespace AplicacaoFaculdade.Models {
    public class Documento {
        public int? documentoId { get; set; }
        public int? documentoTipo { get; set; }
        public string documentoNumero { get; set; }
        public bool? documentoStatus { get; set; }
    }
}
