using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Usuario {
        public int usuarioId { get; set; }
        public string usuarioLogin { get; set; }
        public string usuarioSenha { get; set; }
        public int usuarioFkPessoa { get; set; }
        public bool usuarioStatus { get; set; }
        private Pessoa _pessoa;
        
        public Usuario(Pessoa pessoa){
            this._pessoa = pessoa;
            usuarioFkPessoa = pessoa.pessoaId;
        }

        public Usuario() {}
    }
}