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
    public partial class NovoContratos : System.Web.UI.Page {

        private static DataTable localStorageServicos = new DataTable();
        private ServicoContext servicoContext;
        private DataTable servicoDataTable;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                servicoContext = new ServicoContext();
                LoadServicos();
                if (localStorageServicos.Columns.Count <= 0) {
                    DataColumn column = new DataColumn("servicoNome", typeof(string));
                    DataColumn column2 = new DataColumn("servicoId", typeof(int));
                    DataColumn column3 = new DataColumn("servicoPreco", typeof(float));
                   
                    localStorageServicos.Columns.Add(column);
                    localStorageServicos.Columns.Add(column2);
                    localStorageServicos.Columns.Add(column3);
                } else if (localStorageServicos.Rows.Count > 0) {
                    localStorageServicos.Clear();
                }
                servicosGridView.DataSource = localStorageServicos;
                servicosGridView.DataBind();
            }
        }

        private void LoadServicos() {
            servicoDataTable = servicoContext.GetServicos();
            servicosDropDown.DataSource = servicoDataTable;
            servicosDropDown.DataValueField = "servicoId";
            servicosDropDown.DataTextField = "servicoNome";
            servicosDropDown.DataBound += ServicosDropDown_DataBound;
            servicosDropDown.SelectedIndexChanged += servicosDropDown_SelectedIndexChanged;
            servicosDropDown.DataBind();
        }

        private void ServicosDropDown_DataBound(object sender, EventArgs e) {
            GetServicoSelected();
        }

        protected void servicosDropDown_SelectedIndexChanged(object sender, EventArgs e) {
            GetServicoSelected();
        }

        private void GetServicoSelected() {
            int servicoId = Convert.ToInt32(servicosDropDown.SelectedValue);
            Servico servico = new ServicoContext().GetServicos(new Servico() { Id = servicoId });
            servicoMensalidadeValor.Text = Convert.ToString(servico.precoServicoValor);
        }

        protected void OnRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {

        }
    }
}