using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Aluno : Pessoa {
        public int alunoId { get; set; }
        public int alunoFkPessoa { get; set; }
        private Pessoa pessoa;
        
        public Aluno(Pessoa pessoa) {
            this.pessoa = pessoa;
            alunoFkPessoa = pessoa.pessoaId;
        }
    }
}