using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacaoFaculdade.Models
{
    public class Pessoa{
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Sexo { get; set; }
        public DateTime Nascimento { get; set; }
        public long? Telefone { get; set; }
        public long? Celular { get; set; }
        public long? Cnpj { get; set; }
        public long? Cpf { get; set; }
        public long? Rg { get; set; }
        public string Endereco { get; set; }
        public int? Cep { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool? Status { get; set; }

    }
}