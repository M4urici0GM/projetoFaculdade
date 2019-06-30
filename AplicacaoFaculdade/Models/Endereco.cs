using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models {
    public class Endereco {

        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Unidade { get; set; }

    }
}