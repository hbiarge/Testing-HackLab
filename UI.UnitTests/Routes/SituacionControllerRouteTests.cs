// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SituacionControllerRouteTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the SituacionControllerRouteTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.Routes
{
    using System.Web.Routing;

    using Acheve.UI.App_Start;

    using Xunit;

    public class SituacionControllerRouteTests
    {
        private readonly RouteCollection routes;

        public SituacionControllerRouteTests()
        {
            this.routes = new RouteCollection();
            RouteConfig.RegisterRoutes(this.routes);
        }

        [Fact]
        public void Actual_I()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/",
                new { controller = "Situacion", action = "Actual" });
        }

        [Fact]
        public void Actual_II()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Situacion",
                new { controller = "Situacion", action = "Actual" });
        }

        [Fact]
        public void Actual_III()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Situacion/Actual",
                new { controller = "Situacion", action = "Actual" });
        }

        [Fact]
        public void IniciarJornada()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Situacion/IniciarJornada",
                new { controller = "Situacion", action = "IniciarJornada" });
        }

        [Fact]
        public void TerminarJornada()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Situacion/TerminarJornada",
                new { controller = "Situacion", action = "TerminarJornada" });
        }

        [Fact]
        public void IniciarPausa()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Situacion/IniciarPausa",
                new { controller = "Situacion", action = "IniciarPausa" });
        }

        [Fact]
        public void TerminarPausa()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Situacion/TerminarPausa",
                new { controller = "Situacion", action = "TerminarPausa" });
        }

        [Fact]
        public void JornadaNoIniciada()
        {
            RouteTestHelpers.AssertRoute(
                this.routes,
                "~/Situacion/JornadaNoIniciada",
                new { controller = "Situacion", action = "JornadaNoIniciada" });
        }
    }
}
