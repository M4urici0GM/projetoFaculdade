using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Aluno : Pessoa {
        public int? Id { get; set; }
        public int FkPessoa { get; set; }
        public int? PessoaId { get => base.Id; set => base.Id = value; }
        public bool? PessoaStatus { get => base.Status; set => base.Status = value; }
    }
}