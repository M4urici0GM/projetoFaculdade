using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace AplicacaoFaculdade
{
    public static class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            FriendlyUrlSettings friendlyUrlSettings = new FriendlyUrlSettings();
            friendlyUrlSettings.AutoRedirectMode = RedirectMode.Off;
            routes.EnableFriendlyUrls(friendlyUrlSettings);
        }
    }
}