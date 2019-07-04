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
    public partial class Contratos : System.Web.UI.Page {

        private ServicoContext servicoContext;
        private DataTable contratosDataTable;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                servicoContext = new ServicoContext();
                LoadContratosData();
            }
        }

        private void LoadContratosData() {
            contratosDataTable = servicoContext.GetContratos();
            contratosGridView.DataSource = contratosDataTable;
            contratosGridView.DataBind();
        }
        
        protected void OnRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int contratoID = (int)contratosGridView.DataKeys[gvRow.RowIndex].Value;
            
            if (e.CommandName == "CancelarContrato") {
                Contrato contrato = new ServicoContext().GetContratos(new Contrato() { Id = contratoID });
                if (contrato.Id.HasValue) {
                    contrato.Ativo = false;
                    bool result = new ServicoContext().UpdateContrato(contrato);
                    if (result) {
                        ClientScript.RegisterStartupScript(this.GetType(), "succefullDelete", "<script> Swal.fire({ type: 'success', title: 'ExcluirContrato', text: 'Contrato excluido com sucesso!' }); </script>");
                        return;
                    }
                } 
            }
            ClientScript.RegisterStartupScript(this.GetType(), "errorOnServer", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Houve um erro, favor entrar em contato com o administrador'}); </script>");
            return;
        }
    }
}