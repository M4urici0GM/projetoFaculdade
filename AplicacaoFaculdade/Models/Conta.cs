using System;
namespace AplicacaoFaculdade.Models {
    public class Conta {
        public int contaId { get; set; }
        public string contaNome { get; set; }
        public float contaSaldo { get; set; }
        public bool contaStatus { get; set; }
    }
}
