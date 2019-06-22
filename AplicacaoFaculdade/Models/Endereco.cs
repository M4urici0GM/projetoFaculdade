using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Endereco {
        public int? Id { get; set; }
        public string Logradouro { get; set; }
        public string Desc { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int Cep { get; set; }
        public bool Status { get; set; }
    }
}