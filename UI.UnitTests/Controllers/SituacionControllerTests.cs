// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SituacionControllerTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the SituacionControllerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Acheve.Data.Services.Contracts;
    using Acheve.Domain.Entities;
    using Acheve.Domain.Services.Contracts;
    using Acheve.UI.Controllers;
    using Acheve.UI.UnitTests.AutoFixture;
    using Acheve.UI.ViewModels;

    using FluentAssertions;

    using Moq;

    using Ploeh.AutoFixture.Xunit;

    using Xunit.Extensions;

    public class SituacionControllerTests
    {
        [Theory, AutoHttpData]
        public void ActualSinError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario,
            Jornada jornada)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);
            queriesMock.Setup(x => x.ObtenerUltimaJornada(usuario)).Returns(jornada);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.Actual();

            response.Model.Should().BeOfType<JornadaViewModel>();
            var model = (JornadaViewModel)response.Model;
            model.Jornada.Should().Be(jornada);
            model.Error.Should().BeNull();
        }

        [Theory, AutoHttpData]
        public void ActualConError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario,
            Jornada jornada,
            string error)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);
            queriesMock.Setup(x => x.ObtenerUltimaJornada(usuario)).Returns(jornada);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.TempData["error"] = error;
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.Actual();

            response.Model.Should().BeOfType<JornadaViewModel>();
            var model = (JornadaViewModel)response.Model;
            model.Jornada.Should().Be(jornada);
            model.Error.Should().Be(error);
        }

        [Theory, AutoHttpData]
        public void JornadaNoIniciada(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            IEnumerable<DateTime> jornadas,
            string usuario,
            int max)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);
            queriesMock.Setup(s => s.ObtenerDiasUltimasJornadasRegistradas(usuario, max)).Returns(jornadas);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.UltimasJornadas();

            response.Model.Should().BeOfType<JornadaNoIniciadaViewModel>();
            var model = (JornadaNoIniciadaViewModel)response.Model;
            model.UltimasJornadasRegistradas.Should().BeEquivalentTo(jornadas);
        }

        [Theory, AutoHttpData]
        public void IniciarJornadaSinError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.IniciarJornada();

            response.RouteValues["action"].Should().Be("Actual");
            sut.TempData.Should().BeEmpty();
        }

        [Theory, AutoHttpData]
        public void IniciarJornadaConError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario,
            InvalidOperationException exception)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);
            serviceMock.Setup(s => s.IniciarJornada(usuario)).Throws(exception);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.IniciarJornada();

            response.RouteValues["action"].Should().Be("Actual");
            sut.TempData.Should().ContainKey("error");
            sut.TempData["error"].Should().Be(exception.Message);
        }

        [Theory, AutoHttpData]
        public void TerminarJornadaSinError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.TerminarJornada();

            response.RouteValues["action"].Should().Be("Actual");
            sut.TempData.Should().BeEmpty();
        }

        [Theory, AutoHttpData]
        public void TerminarJornadaConError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario,
            InvalidOperationException exception)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);
            serviceMock.Setup(s => s.TerminarJornada(usuario)).Throws(exception);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.TerminarJornada();

            response.RouteValues["action"].Should().Be("Actual");
            sut.TempData.Should().ContainKey("error");
            sut.TempData["error"].Should().Be(exception.Message);
        }

        [Theory, AutoHttpData]
        public void IniciarPausaSinError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.IniciarPausa();

            response.RouteValues["action"].Should().Be("Actual");
            sut.TempData.Should().BeEmpty();
        }

        [Theory, AutoHttpData]
        public void IniciarPausaConError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario,
            InvalidOperationException exception)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);
            serviceMock.Setup(s => s.IniciarPausa(usuario)).Throws(exception);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.IniciarPausa();

            response.RouteValues["action"].Should().Be("Actual");
            sut.TempData.Should().ContainKey("error");
            sut.TempData["error"].Should().Be(exception.Message);
        }

        [Theory, AutoHttpData]
        public void TerminarPausaSinError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.TerminarPausa();

            response.RouteValues["action"].Should().Be("Actual");
            sut.TempData.Should().BeEmpty();
        }

        [Theory, AutoHttpData]
        public void TerminarPausaConError(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaService> serviceMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario,
            InvalidOperationException exception)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);
            serviceMock.Setup(s => s.TerminarPausa(usuario)).Throws(exception);

            var sut = new SituacionController(serviceMock.Object, queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.TerminarPausa();

            response.RouteValues["action"].Should().Be("Actual");
            sut.TempData.Should().ContainKey("error");
            sut.TempData["error"].Should().Be(exception.Message);
        }
    }
}
