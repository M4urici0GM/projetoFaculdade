using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Pessoa{
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public bool Juridica { get; set; }
        public bool Sexo { get; set; }
        public DateTime Nascimento { get; set; }
        public bool? Status { get; set; }
    }
}