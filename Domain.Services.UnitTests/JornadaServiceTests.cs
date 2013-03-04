// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaServiceTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaServiceTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Services.UnitTests
{
    using System;

    using Acheve.Data.Services.Contracts;
    using Acheve.Domain.Entities;
    using Acheve.Domain.Services;
    using Acheve.Infrastructure.Services.Contracts;

    using global::Domain.Services.UnitTests.AutoFixture;

    using FluentAssertions;

    using Moq;

    using Ploeh.AutoFixture.Xunit;

    using Xunit.Extensions;

    public class JornadaServiceTests
    {
        [Theory, AutoMoqData]
        public void NoSePuedeIniciarJornadaConJornadaIniciada(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            string usuario)
        {
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);

            Action action = () => sut.IniciarJornada(usuario);

            action.ShouldThrow<InvalidOperationException>()
                .WithMessage("Ya existe una jornada iniciada. Debe terminar la jornada iniciada antes de iniciar una nueva.");
            commandsMock.Verify(c => c.CrearJornada(It.IsAny<Jornada>(), usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeIniciarJornadaSiYaExisteJornadaParaElNuevoDia(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            DateTime finJornada,
            DateTime diaNuevaJornada,
            string usuario)
        {
            jornada.Finalizar(finJornada);
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            queriesMock.Setup(q => q.ExisteJornada(usuario, diaNuevaJornada)).Returns(true);
            timeProviderMock.Setup(t => t.Today).Returns(diaNuevaJornada);

            Action action = () => sut.IniciarJornada(usuario);

            action.ShouldThrow<InvalidOperationException>()
                .WithMessage("Ya existe una jornada para la fecha indicada.");
            commandsMock.Verify(c => c.CrearJornada(It.IsAny<Jornada>(), usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void SePuedeIniciarJornadaConVerificacionesCorrectas(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            DateTime finJornada,
            DateTime diaNuevaJornada,
            string usuario)
        {
            jornada.Finalizar(finJornada);
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            queriesMock.Setup(q => q.ExisteJornada(usuario, diaNuevaJornada)).Returns(false);
            timeProviderMock.Setup(t => t.Now).Returns(diaNuevaJornada);

            sut.IniciarJornada(usuario);

            commandsMock.Verify(c => c.CrearJornada(It.IsAny<Jornada>(), usuario), Times.Once());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeTerminarJornadaSiLaUltimaJornadaEstaTerminada(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            DateTime finJornada,
            string usuario)
        {
            jornada.Finalizar(finJornada);
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(8));

            Action accion = () => sut.TerminarJornada(usuario);

            accion.ShouldThrow<InvalidOperationException>()
                .WithMessage("No se puede terminar un periodo ya terminado.");
            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeTerminarJornadaSiLaJornadaTienePausaIniciada(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            DateTime inicioPausa,
            string usuario)
        {
            jornada.IniciarPausa(inicioPausa);
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(8));

            Action accion = () => sut.TerminarJornada(usuario);

            accion.ShouldThrow<InvalidOperationException>()
                .WithMessage("No se puede terminar una jornada con una pausa iniciada.");
            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeTerminarJornadaConFechaFinMenorQueFechaInicio(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            string usuario)
        {
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(-1));

            Action accion = () => sut.TerminarJornada(usuario);

            accion.ShouldThrow<InvalidOperationException>()
                .WithMessage("La fecha de finalización debe ser posterior a la fecha de inicio.");
            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void SePuedeTerminarJornadaEnCondicionesCorrectas(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            string usuario)
        {
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(8));

            sut.TerminarJornada(usuario);

            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Once());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeIniciarPausaSiUltimaJOrnadaTerminada(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            DateTime finJornada,
            string usuario)
        {
            jornada.Finalizar(finJornada);
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(1));

            Action accion = () => sut.IniciarPausa(usuario);

            accion.ShouldThrow<InvalidOperationException>()
                .WithMessage("No se puede iniciar una pausa en una Jornada finalizada.");
            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeIniciarPausaSiYaExistePausaIniciada(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            DateTime inicioPausa,
            string usuario)
        {
            jornada.IniciarPausa(inicioPausa);
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(1));

            Action accion = () => sut.IniciarPausa(usuario);

            accion.ShouldThrow<InvalidOperationException>()
                .WithMessage("No se puede iniciar una pausa cuando ya hay una pausa iniciada.");
            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeIniciarPausaConFechaAnteriorAlInicioDeLaJornada(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            string usuario)
        {
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(-1));

            Action accion = () => sut.IniciarPausa(usuario);

            accion.ShouldThrow<InvalidOperationException>()
                .WithMessage("No se puede iniciar una pausa en una fecha anterior al inicio de la jornada.");
            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void SePuedeIniciarPausaEnLasCondicinesCorrectas(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            string usuario)
        {
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(1));

            sut.IniciarPausa(usuario);

            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Once());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeTerminarPausaSiLaUltimaJornadaEstaTerminada(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            DateTime finJornada,
            string usuario)
        {
            jornada.Finalizar(finJornada);
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(1));

            Action accion = () => sut.TerminarPausa(usuario);

            accion.ShouldThrow<InvalidOperationException>()
                .WithMessage("No se puede terminar una pausa en una Jornada finalizada.");
            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeTerminarPausaSiNoHayPausaIniciada(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            string usuario)
        {
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(jornada.Inicio.AddHours(1));

            Action accion = () => sut.TerminarPausa(usuario);

            accion.ShouldThrow<InvalidOperationException>()
                .WithMessage("No se puede terminar una pausa cuando no hay una pausa iniciada.");
            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void NoSePuedeTerminarPausaConFechaAnteriorAInicioPausa(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            DateTime inicioPausa,
            string usuario)
        {
            jornada.IniciarPausa(inicioPausa);
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(inicioPausa.AddHours(-1));

            Action accion = () => sut.TerminarPausa(usuario);

            accion.ShouldThrow<InvalidOperationException>()
                .WithMessage("La fecha de finalización debe ser posterior a la fecha de inicio.");
            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Never());
        }

        [Theory, AutoMoqData]
        public void SePuedeTerminarPausaEnCondicionesCorrectas(
            [Frozen]Mock<IJornadaQueries> queriesMock,
            [Frozen]Mock<IJornadaCommands> commandsMock,
            [Frozen]Mock<ITimeProvider> timeProviderMock,
            JornadaService sut,
            Jornada jornada,
            DateTime inicioPausa,
            string usuario)
        {
            jornada.IniciarPausa(inicioPausa);
            queriesMock.Setup(q => q.ObtenerUltimaJornada(usuario)).Returns(jornada);
            timeProviderMock.Setup(t => t.Now).Returns(inicioPausa.AddHours(1));

            sut.TerminarPausa(usuario);

            commandsMock.Verify(c => c.ActualizarJornada(jornada, usuario), Times.Once());
        }
    }
}
