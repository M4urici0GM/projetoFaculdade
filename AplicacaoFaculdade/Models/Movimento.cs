using System;
namespace AplicacaoFaculdade.Models {
    public class Movimento {
        public int movimentoId { get; set; }
        public string movimentoTipo { get; set; }
        public string movimentoOrigem { get; set; }
        public float movimentoValor { get; set; }
        public int movimentoFkConta { get; set; }
        public int movimentoFkPessoa { get; set; }
        public DateTime movimentoDataEmissao { get; set; }
        public DateTime movimentoDataPagamento { get; set; }
    }
}
