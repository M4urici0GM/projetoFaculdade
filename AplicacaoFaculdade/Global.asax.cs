using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.Optimization;

namespace AplicacaoFaculdade
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e) {
            RegisterRoutes(RouteTable.Routes);  //Register roject routes
            RegisterAPIRoutes(RouteTable.Routes); //Register API Routes
        }

        private void RegisterRoutes(RouteCollection route){
            route.MapPageRoute("home", "", "~/Views/User/Index.aspx"); //Default project route
            route.MapPageRoute("adminLogin", "admin/login", "~/Views/Admin/Login.aspx"); //Default project route
            route.MapPageRoute("adminHome", "admin", "~/Views/Admin/Index.aspx");
            route.MapPageRoute("usuarios", "admin/usuarios", "~/Views/Admin/Usuarios.aspx");
            route.MapPageRoute("editarUsuario", "admin/usuarios/editar/{usuarioId}", "~/Views/Admin/EditarUsuario.aspx");
            route.MapPageRoute("novoUsuario", "admin/usuarios/novo", "~/Views/Admin/CadastrarUsuario.aspx");
            route.MapPageRoute("pessoas", "admin/pessoas", "~/Views/Admin/Pessoas.aspx");
            route.MapPageRoute("novaPessoa", "admin/pessoas/cadastrar", "~/Views/Admin/CadastrarPessoa.aspx");
        }

        private void RegisterAPIRoutes(RouteCollection routes) {

        }
    }
}