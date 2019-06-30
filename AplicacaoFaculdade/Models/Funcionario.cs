using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Funcionario : Pessoa{

        public int? Id { get; set; }
        public int? FkPessoa { get => base.Id; set => base.Id = value; }
        public bool? Status { get; set; }
        public int? FkCargo { get; set; }
        public bool? PessoaStatus { get => base.Status; set => base.Status = value; }
       
    }
}