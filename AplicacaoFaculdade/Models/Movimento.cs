using System;
namespace AplicacaoFaculdade.Models {
    public class Movimento {
        public int? Id { get; set; }
        public string Tipo { get; set; }
        public string Origem { get; set; }
        public float Valor { get; set; }
        public int? FkConta { get; set; }
        public int? FkPessoa { get; set; }
        public int? FkUsuario { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool? Status { get; set; }

    }
}
