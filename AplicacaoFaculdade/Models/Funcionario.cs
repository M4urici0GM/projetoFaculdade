using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Funcionario{
        public Pessoa pessoa;
        public Cargo cargo;
        public int funcionarioId { get; set; }
        public int funcionarioFkPessoa { get; }
        public bool funcionarioStatus { get; set; }
        public int funcionarioFkCargo { get; set; }
        

        public Funcionario(Pessoa pessoa, Cargo cargo) {
            this.pessoa = pessoa;
            this.cargo  = cargo;
            funcionarioFkPessoa = pessoa.pessoaId;
            funcionarioFkCargo = cargo.cargoId;
        }
    }
}