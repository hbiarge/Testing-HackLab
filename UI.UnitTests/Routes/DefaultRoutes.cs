// --------------------------------------------------------------public ------------------------------------------------------
// <copyright file="DefaultRoutes.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the DefaultRoutes type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.Routes
{
    using System.Web.Routing;

    using Acheve.UI.App_Start;

    using Xunit;

    public class DefaultRoutes
    {
        private readonly RouteCollection routes;

        public DefaultRoutes()
        {
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [Fact]
        public void NoControllerNoAction()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/",
                new { controller = "Cuenta", action = "LogOn" });
        }

        [Fact]
        public void SiControllerNoAction()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Cuenta",
                new { controller = "Cuenta", action = "LogOn" });
        }
    }
}
