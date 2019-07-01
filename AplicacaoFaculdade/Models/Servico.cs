using System;
namespace AplicacaoFaculdade.Models {
    public class Servico : PrecoServico {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public bool? Status { get; set; }
    }
}