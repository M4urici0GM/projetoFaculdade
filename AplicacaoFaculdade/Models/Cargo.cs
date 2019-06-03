using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Cargo {
        public int cargoId { get; set; }
        public string cargoNome { get; set; }
        public float cargoSalario { get; set; }
        public bool  cargoStatus { get; set; }
    }
}