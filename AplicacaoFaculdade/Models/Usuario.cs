using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Usuario : Pessoa{
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? FkPessoa { get; set; }
        public bool? Status { get; set; }
        public int FkNivelAcesso { get; set; }
        public int? PessoaId { get => base.Id;  set => base.Id = value; }
    }
}