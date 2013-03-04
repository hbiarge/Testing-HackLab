// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the MvcApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI
{
    using System;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Acheve.UI.App_Start;
    using Acheve.UI.Infrastructure;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var container = IoCConfig.Configure();
            SecurityConfig.Configure(container);

            ModelBinders.Binders.Add(typeof(DateTime), new LocaleDateTimeBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new LocaleDateTimeBinder());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}