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
            route.MapPageRoute("home", "", "~/Views/Index.aspx"); //Default project route
            route.MapPageRoute("login", "login", "~/Views/Login.aspx"); //Default project route
            route.MapPageRoute("usuarios", "usuarios", "~/Views/Usuarios.aspx");
        }

        private void RegisterAPIRoutes(RouteCollection routes) {

        }
    }
}