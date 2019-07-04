using AplicacaoFaculdade.DatabaseContext;
using AplicacaoFaculdade.Models;
using System;
using System.Data;
using AplicacaoFaculdade.Enums;
using System.Globalization;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class MovimentoFinanceiro : System.Web.UI.Page {

        private FinanceiroContext financeiroContext;
        private DataTable movimentosDataTable;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                financeiroContext = new FinanceiroContext();
                LoadMovimentosDados();
            }
        }

        private void LoadMovimentosDados() {
            movimentosDataTable = financeiroContext.GetMovimentos();
            movimentoGridView.DataSource = movimentosDataTable;
            movimentoGridView.DataBind();
        }

        protected void OnFiltrarClickEventHandler(object sender, EventArgs e) {
            financeiroContext = new FinanceiroContext();
            if (filtroDataInicial.Text.Equals("") || filtroDataFinal.Text.Equals("")) {
                ClientScript.RegisterStartupScript(this.GetType(), "missingFields", "<script>Swal.fire({ type: 'error', title: 'Ops, algo está faltando', text: 'Para filtrar, preencha todos os dados' });</script>");
                return;
            }

            financeiroContext = new FinanceiroContext();
            DateTime dataInicial = DateTime.ParseExact(filtroDataInicial.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            DateTime dataFinal = DateTime.ParseExact(filtroDataFinal.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            int movimentoOrigem = Convert.ToInt32(movimentoOrigemDropDown.Text);
            int movimentoTipo = Convert.ToInt32(movimentoTipoDropDown.Text);
            int movimentoStatus = Convert.ToInt32(movimentoStatusDropDown.Text);
            movimentosDataTable = financeiroContext.GetMovimentos(dataInicial, dataFinal, movimentoOrigem, movimentoTipo, movimentoStatus);
            movimentoGridView.DataSource = movimentosDataTable;
            movimentoGridView.DataBind();
        }
    }
}