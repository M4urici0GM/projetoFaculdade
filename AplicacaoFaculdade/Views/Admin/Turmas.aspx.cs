using System;
using System.Data;
using AplicacaoFaculdade.Models;
using AplicacaoFaculdade.DatabaseContext;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class Turmas : System.Web.UI.Page {

        private DataTable turmasDataTable;
        private ServicoContext servicoContext;
        private static DataTable localTurmaDiasStorage = new DataTable();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                servicoContext = new ServicoContext();
                GetTurmasData();
            }
        }

        private void GetTurmasData() {
            turmasDataTable = servicoContext.GetTurmas();
            turmasGridView.DataSource = turmasDataTable;
            turmasGridView.DataBind();
        }

        protected void OnRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {

        }

        protected void OnPageChangingIndex(object sender, GridViewPageEventArgs e) {

        }


    }
}