using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Usuario {
        public int? usuarioId { get; set; }
        public string usuarioEmail { get; set; }
        public string usuarioSenha { get; set; }
        public int? usuarioFkPessoa { get; set; }
        public bool? usuarioStatus { get; set; }
        public int? usuarioFkNivelAcesso { get; set; }

        public Pessoa pessoa;
        
        public Usuario(Pessoa pessoa){
            this.pessoa = pessoa;
            usuarioFkPessoa = pessoa.pessoaId;
        }

        public Usuario() {}
    }
}