// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteTestHelpers.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the RouteTestHelpers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.Routes
{
    using System;
    using System.Web;
    using System.Web.Routing;

    using Moq;

    using Xunit;

    public static class RouteTestHelpers
    {
        public static void AssertRoute(RouteCollection routes, string url, object expectations)
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns(url);

            var routeData = routes.GetRouteData(httpContextMock.Object);
            Assert.NotNull(routeData);

            foreach (var kvp in new RouteValueDictionary(expectations))
            {
                Assert.True(
                    string.Equals(
                        kvp.Value.ToString(), routeData.Values[kvp.Key].ToString(), StringComparison.OrdinalIgnoreCase),
                    string.Format("Expected '{0}', not '{1}' for '{2}'.", kvp.Value, routeData.Values[kvp.Key], kvp.Key));
            }
        }
    }
}
