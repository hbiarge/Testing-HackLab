// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the RouteConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.App_Start
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Situacion", action = "Actual", id = UrlParameter.Optional },
                namespaces: new string[] { "Acheve.UI.Controllers" });
        }
    }
}