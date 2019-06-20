using System;
namespace AplicacaoFaculdade.Models {
    public class PrecoServico {
        public int precoServicoId { get; set; }
        public float precoServicoValor { get; set; }
        public DateTime precoServicoDataInicial { get; set; }
        public DateTime precoServicoDataFinal { get; set; }
        public int precoServicoFkServico { get; set; }
    }
}
