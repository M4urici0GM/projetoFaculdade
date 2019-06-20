using System;
namespace AplicacaoFaculdade.Models {
    public class DataLog {
        public int dataLogId { get; set; }
        public DateTime dataLogData { get; set; }
        public int dataLogFkUsuario { get; set; }
        public string dataLogAtividade { get; set; }
    }
}
