// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the WebApiConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.App_Start
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
