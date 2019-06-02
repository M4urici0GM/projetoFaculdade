using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Pessoa{
        public int pessoaId { get; set; }
        public string pessoaNome { get; set; }
        public string pessoaSobreNome { get; set; }
        public bool pessoaJuridica { get; set; }
        public bool pessoaSexo { get; set; }
        public DateTime pessoaNascimento { get; set; }
        public bool pessoaStatus { get; set; }
    }
}