using System;
using System.Web.UI.WebControls;
using AplicacaoFaculdade.DatabaseContext;
using System.Data;
using AplicacaoFaculdade.Models;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class Pessoas : System.Web.UI.Page {

        private PessoaContext pessoaContext;
        private DataTable pessoasDataTable;
        
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                pessoaContext = new PessoaContext();
                LoadPessoaData();
            }
        }

        private void LoadPessoaData() {
            pessoasDataTable = pessoaContext.GetPessoas();
            pessoaGridView.DataSource = pessoasDataTable;
            pessoaGridView.DataBind();
        }

        protected void OnRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gridViewRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int pessoaId = (int)pessoaGridView.DataKeys[gridViewRow.RowIndex].Value;
            if (e.CommandName == "editarPessoa") {
                Response.RedirectToRoute("editarPessoa", new { pessoaId });
            } else if (e.CommandName == "excluirUsuario") {
                PessoaContext pessoaContext = new PessoaContext();
                Pessoa pessoa = pessoaContext.GetPessoa(pessoaId);
                pessoa.Status = false;
                bool editarPessoa = pessoaContext.UpdatePessoa(pessoa);
                if (editarPessoa && pessoaContext.AffectedRows > 0) {
                    ClientScript.RegisterStartupScript(this.GetType(), "successDeleted", "<script> Swal.fire({type: 'success', title: 'Excluir pessoa', text: 'Registro excluido com sucesso!'}).then(() => location.href = '/admin/pessoas'); </script>");
                } else {
                    ClientScript.RegisterStartupScript(this.GetType(), "errorOnDelete", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve algum erro, favor entrar com o administrador do sistema'}); </script>");
                }
            }
 
        }

        protected void OnPageChangingIndex(object sender, GridViewPageEventArgs e) {
            LoadPessoaData();
            pessoaGridView.PageIndex = e.NewPageIndex;
            pessoaGridView.DataBind();
        }

    }
}