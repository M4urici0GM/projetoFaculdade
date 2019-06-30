using AplicacaoFaculdade.DatabaseContext;
using AplicacaoFaculdade.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class Alunos : System.Web.UI.Page {

        private PessoaContext pessoaContext;
        private DataTable alunosDataTable;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                pessoaContext = new PessoaContext();
                GetAlunosData();
                pesquisaPessoaIdHidden.Text = "";
                pessoaPesquisaNome.Text = "";
            }
        }

        private void GetAlunosData() {
            alunosDataTable = pessoaContext.GetAlunos();
            HandleDataToGridView();
        }

        private void HandleDataToGridView() {
            alunosGriView.DataSource = alunosDataTable;
            alunosGriView.DataBind();
        }

        protected void AlunosRowCommandClickEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int alunoId = (int) alunosGriView.DataKeys[gvRow.RowIndex].Value;
            if (e.CommandName == "desativarAluno") {
                pessoaContext = new PessoaContext();
                Aluno aluno = pessoaContext.GetAluno(new Aluno() { Id = alunoId });
                aluno.Status = false;
                bool result = new PessoaContext().UpdateAluno(aluno);
                if (result) {
                    ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type: 'success', title: 'Excluir aluno', text: 'Aluno excluido com sucesso!' }).then(() => location.href = '/admin/pessoas/alunos'); </script>");
                } else {
                    ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador' }); </script>");
                }
            } else {

            }
        }

        protected void OnPageChangingIndex(object sender, GridViewPageEventArgs e) {

        }

        protected void AdicionarAlunoSaveClickEventHandler(object sender, EventArgs e) {
            pessoaContext = new PessoaContext();
            if (pesquisaPessoaIdHidden.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingValues", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando!', text: 'Para adicionar um novo aluno, por favor, efetue a pesquisa antes!' }); </script>");
                return;
            }
            int pessoaId = Convert.ToInt32(pesquisaPessoaIdHidden.Text);
            int result = pessoaContext.CreateAluno(new Pessoa() { Id = pessoaId });
            if (result == 1) {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type: 'success', title: 'Inserir aluno', text: 'Aluno inserido com sucesso!' }).then(() => location.href = '/admin/pessoas/alunos'); </script>");
            } else if (result == 2) {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type: 'error', title: 'Ops, algo está errado', text: 'Essa pessoa já está definida como aluno' }); </script>");
            } else {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({ type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador' }); </script>");
            }
        }

        protected void PesquisarPessoaClickEventHandler(object sender, EventArgs e) {
            pessoaContext = new PessoaContext();
            if (pessoaPesquisaTxt.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "pesquisarPessoaInvalidInput", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Preencha o documento para pesquisar uma pessoa!' }); </script>");
                return;
            }
            long documentoNumero = Convert.ToInt64(pessoaPesquisaTxt.Text);
            Pessoa pessoa = pessoaContext.GetPessoa(documentoNumero);
            if (pessoa.Id.HasValue) {
                pesquisaPessoaIdHidden.Text = Convert.ToString(pessoa.Id.Value);
                pessoaPesquisaNome.Text = pessoa.Nome + " " + pessoa.Sobrenome;
            } else {
                ClientScript.RegisterStartupScript(this.GetType(), "pesquisarPessoaInvalidInput", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'Não foram encontrados registros com esse documento!' }); </script>");
            }
        }
    }
}