using AplicacaoFaculdade.Models;
using System;
using System.Web.UI.WebControls;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using AplicacaoFaculdade.DatabaseContext;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class CadastrarPessoa : System.Web.UI.Page {



        private PessoaContext pessoaContext;

        protected void Page_Load(object sender, EventArgs e) {
            pessoaContext = new PessoaContext();
        }

        protected void CadastrarPessoaClickEventHandler(object sender, EventArgs e) {
            if (pessoaNome.Text.Equals("") || pessoaSobrenome.Text.Equals("") || pessoaSexo.Text.Equals("") || pessoaCpf.Text.Equals("") || pessoaEndereco.Text.Equals("") || pessoaCep.Text.Equals("") || pessoaNumeroRua.Text.Equals("") || pessoaCidade.Text.Equals("") || pessoaEstado.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnLogin", "<script> Swal.fire({type: 'error', title: 'Ops, algo está faltando', text: 'Por favor, preencha todos os dados obrigatórios!'}); </script>");
                return;
            }
            Pessoa pessoa = new Pessoa() {
                Nome = pessoaNome.Text,
                Sobrenome = pessoaSobrenome.Text,
                Sexo = Convert.ToInt32(pessoaSexo.SelectedValue),
                Nascimento = DateTime.Parse(pessoaNascimento.Text),
                Telefone = string.IsNullOrEmpty(pessoaTelefone.Text) ? (long?)null : Convert.ToInt64(pessoaTelefone.Text),
                Celular = string.IsNullOrEmpty(pessoaCelular.Text) ? (long?)null : Convert.ToInt64(pessoaCelular.Text),
                Rg = string.IsNullOrEmpty(pessoaRg.Text) ? (long?)null : Convert.ToInt64(pessoaRg.Text),
                Cpf = string.IsNullOrEmpty(pessoaCpf.Text) ? (long?)null : Convert.ToInt64(pessoaCpf.Text),
                Cnpj = string.IsNullOrEmpty(pessoaCnpj.Text) ? (long?)null : Convert.ToInt64(pessoaCnpj),
                Cep = Convert.ToInt32(pessoaCep.Text),
                Cidade = pessoaCidade.Text,
                Complemento = pessoaComplemento.Text,
                Endereco = pessoaEndereco.Text,
                Estado = pessoaEstado.Text,
                Numero = string.IsNullOrEmpty(pessoaNumeroRua.Text) ? (int?)null : Convert.ToInt32(pessoaNumeroRua.Text),
            };
            bool cadastrarPessoa = pessoaContext.CadastrarPessoa(pessoa);
            if (cadastrarPessoa && pessoaContext.LastInserted.HasValue) {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnLogin", "<script> Swal.fire({type: 'success', title: 'Cadastrar pessoa', text: 'Pessoa cadastrada com sucesso!'}).then(() => location.href = '/admin/pessoas/'); </script>");
            } else {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnLogin", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve algum erro, favor entrar com o administrador do sistema'}); </script>");
            }
        }
    }
}