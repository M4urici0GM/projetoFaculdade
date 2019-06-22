using System;
namespace AplicacaoFaculdade.Models {
    public class Documento {
        public int? Id { get; set; }
        public int? Tipo { get; set; }
        public string Numero { get; set; }
        public bool? Status { get; set; }
    }
}
