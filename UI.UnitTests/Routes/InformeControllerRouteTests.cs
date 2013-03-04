// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformeControllerRouteTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InformeControllerRouteTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.Routes
{
    using System.Web.Routing;

    using Acheve.UI.App_Start;

    using Xunit;

    public class InformeControllerRouteTests
    {
        private readonly RouteCollection routes;

        public InformeControllerRouteTests()
        {
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [Fact]
        public void Index()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Informe/Index",
                new { controller = "Informe", action = "Index" });
        }

        [Fact]
        public void Dia()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Informe/Dia",
                new { controller = "Informe", action = "Dia" });
        }

        [Fact]
        public void EntreFechas()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Informe/EntreFechas",
                new { controller = "Informe", action = "EntreFechas" });
        }
    }
}
