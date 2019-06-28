using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views.template {
    public partial class Site1 : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            var session = Session["usuarioLogin"];

            if (session == null) {
                Response.RedirectToRoute("adminLogin");
            }
        }

        public void DoLogout(object sender, EventArgs e) {
            Session.Abandon();
            Response.RedirectToRoute("home");
        }
    }
}