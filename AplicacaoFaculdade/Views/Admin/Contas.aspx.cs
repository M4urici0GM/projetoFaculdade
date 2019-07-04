using System;
using System.Data;
using System.Web.UI.WebControls;
using AplicacaoFaculdade.DatabaseContext;
using AplicacaoFaculdade.Models;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class Contas : System.Web.UI.Page {

        private FinanceiroContext financeiroContext;
        private DataTable contasDataTable;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                financeiroContext = new FinanceiroContext();
                LoadContasData();
            }
        }

        private void LoadContasData() {
            contasDataTable = financeiroContext.GetContas();
            contasGridView.DataSource = contasDataTable;
            contasGridView.DataBind();
        }

        protected void OnRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gridViewRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int contaId = (int)contasGridView.DataKeys[gridViewRow.RowIndex].Value;
            if (e.CommandName == "excluirConta") {
                Conta conta = new FinanceiroContext().GetContas(new Conta() { Id = contaId });
                if (conta.Id.HasValue) {
                    if (conta.Saldo > 0) {
                        ClientScript.RegisterStartupScript(this.GetType(), "error", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta errado', text: 'Nao é possivel excluir uma conta que contem saldo.' }); </script>");
                        return;
                    } else {
                        conta.Status = false;
                        bool result = new FinanceiroContext().UpdateConta(conta);
                        if (result) {
                            ClientScript.RegisterStartupScript(this.GetType(), "error", "<script> Swal.fire({ type: 'success', title: 'Excluir conta', text: 'Conta excluida com sucesso.' }).then(() => location.href='" + GetRouteUrl("contas", null) + "'); </script>");
                            return;
                        }
                    }
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador'}); </script>");
            return;
        }

        protected void OnAdicionarContaClickEventHandler(object sender, EventArgs e) {
            if (contaNome.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script> Swal.fire({ type: 'error', title: 'Ops, algo esta faltando', text: 'O nome da conta é obrigatório.' }); </script>");
                return;
            }
            Conta conta = new Conta() {
                Nome = contaNome.Text,
                Saldo = (float)Convert.ToDouble(contaSaldo.Text),
                Status = true
            };
            bool result = new FinanceiroContext().CreateConta(conta);
            if (result) {
                ClientScript.RegisterStartupScript(this.GetType(), "error", "<script> Swal.fire({ type: 'success', title: 'Cadastrar conta', text: 'Conta cadastrada com sucesso.' }).then(() => location.href='" + GetRouteUrl("contas", null) + "'); </script>");
                ;
                return;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador'}); </script>");
            return;
        }
    }
}