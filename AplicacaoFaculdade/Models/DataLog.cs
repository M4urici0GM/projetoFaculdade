using System;
namespace AplicacaoFaculdade.Models {
    public class DataLog {
        public int? Id { get; set; }
        public DateTime Data { get; set; }
        public int FkUsuario { get; set; }
        public string Atividade { get; set; }
    }
}
