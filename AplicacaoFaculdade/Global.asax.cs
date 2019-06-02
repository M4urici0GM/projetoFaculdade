using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
using System.Web.SessionState;



namespace AplicacaoFaculdade
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e) {
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterRoutes(RouteTable.Routes);
        }

        public void RegisterRoutes(RouteCollection route){
            route.MapPageRoute("home", "", "~/Views/Default.aspx"); //Default project route
        }
    }
}