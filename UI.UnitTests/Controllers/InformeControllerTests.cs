// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformeControllerTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InformeControllerTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Acheve.Data.Services.Contracts;
    using Acheve.Domain.Entities;
    using Acheve.UI.Controllers;
    using Acheve.UI.UnitTests.AutoFixture;
    using Acheve.UI.ViewModels;

    using FluentAssertions;

    using Moq;

    using Ploeh.AutoFixture.Xunit;

    using Xunit.Extensions;

    public class InformeControllerTests
    {
        [Theory, AutoHttpData]
        public void DiaConFechaValida(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario,
            Jornada jornada)
        {
            identityMock.Setup(x => x.Name).Returns(usuario);
            queriesMock.Setup(x => x.ObtenerJornada(usuario, jornada.Inicio.Date)).Returns(jornada);

            var sut = new InformeController(queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.Dia(new CriteriosBusquedaFechaViewModel { Fecha = jornada.Inicio.Date });

            response.Model.Should().BeOfType<InformeJornadaViewModel>();
            var model = (InformeJornadaViewModel)response.Model;
            model.Jornada.Should().Be(jornada);
        }

        [Theory, AutoHttpData]
        public void EntreFechasConFechasValidas(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IIdentity> identityMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario,
            ResumenJornada[] resumenes)
        {
            var inicio = resumenes.First().Dia.Date;
            var fin = resumenes.Last().Dia.Date;
            identityMock.Setup(x => x.Name).Returns(usuario);
            queriesMock
                .Setup(x => x.ObtenerResumenEntreFechas(usuario, inicio, fin))
                .Returns(resumenes);

            var sut = new InformeController(queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);

            var response = sut.EntreFechas(new CriteriosBusquedaEntreFechasViewModel { Inicio = inicio, Fin = fin });

            response.Should().BeOfType<ViewResult>();
            response.Model.Should().BeOfType<InformeResumenEntreFechasViewModel>();
            var model = (InformeResumenEntreFechasViewModel)response.Model;
            model.ResumenJornadas.Should().BeEquivalentTo(resumenes);
        }

        [Theory, AutoHttpData]
        public void EntreFechasConFechasNoValidas(
            [Frozen]Mock<HttpContextBase> httpContextMock,
            [Frozen]Mock<IJornadaQueries> queriesMock,
            string usuario,
            DateTime fin,
            DateTime inicio)
        {
            var sut = new InformeController(queriesMock.Object);
            sut.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), sut);
            sut.ModelState.AddModelError("Inicio", "Fin menor que Inicio");

            var response = sut.EntreFechas(new CriteriosBusquedaEntreFechasViewModel { Inicio = inicio, Fin = fin });

            response.Should().BeOfType<ViewResult>();
            response.Model.Should().BeOfType<InformeResumenEntreFechasViewModel>();
            var model = (InformeResumenEntreFechasViewModel)response.Model;
            model.ResumenJornadas.Should().BeNull();
        }
    }
}
