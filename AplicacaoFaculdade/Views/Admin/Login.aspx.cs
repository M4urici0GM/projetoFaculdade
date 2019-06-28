using AplicacaoFaculdade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacaoFaculdade.Views {
    public partial class Login : System.Web.UI.Page {
        UsuarioContext context = new UsuarioContext();

        protected void Page_Load(object sender, EventArgs e) { }

        protected async void DoLoginEvent(object sender, EventArgs e) {
            string usuarioEmailStr = usuarioEmail.Text;
            string usuarioSenhaStr = usuarioSenha.Text;
            Usuario usuario = await context.DoLogin(usuarioEmailStr, usuarioSenhaStr);
            if (usuario.Id.HasValue) {
                Session[ "usuarioLogin" ] = usuario;
                Response.RedirectToRoute("adminHome");
            } else {
                ClientScript.RegisterStartupScript(this.GetType(), "errorOnLogin", "<script> Swal.fire({type: 'error', title: 'Ops, algo deu errado', text: 'Usuario ou senha invalidos!'}); </script>");
            }
        }

    }
}