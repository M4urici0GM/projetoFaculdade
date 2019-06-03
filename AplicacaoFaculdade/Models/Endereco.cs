using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Endereco {
        public int enderecoId { get; set; }
        public string enderecoLogradouro { get; set; }
        public string enderecoDesc { get; set; }
        public int enderecoNumero { get; set; }
        public string enderecoBairro { get; set; }
        public string enderecoCidade { get; set; }
        public int enderecoCep { get; set; }
        public bool enderecoStatus { get; set; }
    }
}