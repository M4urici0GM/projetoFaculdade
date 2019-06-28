using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.Admin {
    public partial class Usuarios : System.Web.UI.Page {
        private UsuarioContext usuarioContext;
        protected void Page_Load(object sender, EventArgs e) {
            usuarioContext = new UsuarioContext();

            if (!IsPostBack)
                LoadUserData();
        }

        private void LoadUserData() {
            DataTable userDataTable = usuarioContext.GetUsuarios();
            userGridView.DataSource = userDataTable;
            userGridView.DataBind();
            
        }

        public void OnRowCommandEventHandler(object sender, GridViewCommandEventArgs e) {
            GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int usuarioId = (int) userGridView.DataKeys[gvRow.RowIndex].Value;
            Response.RedirectToRoute($"editarUsuario", new { usuarioId });
        }

        public void OnPageChangingIndex(object sender, GridViewPageEventArgs e) {
            LoadUserData();
            userGridView.PageIndex = e.NewPageIndex;
            userGridView.DataBind();
        }
    }
}