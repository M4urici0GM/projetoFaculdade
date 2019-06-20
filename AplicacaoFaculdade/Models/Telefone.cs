using System;
namespace AplicacaoFaculdade.Models {
    public class Telefone {
        public int telefoneId { get; set; }
        public int telefoneDDD { get; set; }
        public int telefoneNumero { get; set; }
        public bool telefonePadrao { get; set; }
        public int telefoneFkTipo { get; set; }
    }
}
