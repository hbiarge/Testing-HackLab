// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Entities.UnitTests
{
    using System;

    using Acheve.Domain.Entities;

    using FluentAssertions;

    using Ploeh.AutoFixture;

    using Xunit;

    public class JornadaTests
    {
        [Fact]
        public void NoSePuedeCrearJornadaIniciadaConPausasAnterioresAFechaInicio()
        {
            var fixture = new Fixture();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa, finPausa);

            Assert.Throws<InvalidOperationException>(
                () => new Jornada(inicioJornada, pausa));
        }

        [Fact]
        public void NoSePuedeCrearJornadaTerminadaConPausasFueraDeLasFechasDeLaJornada1()
        {
            var fixture = new Fixture();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();
            var finJornada = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa, finPausa);

            Assert.Throws<InvalidOperationException>(
                () => new Jornada(inicioJornada, finJornada, pausa));
        }

        [Fact]
        public void NoSePuedeCrearJornadaTerminadaConPausasFueraDeLasFechasDeLaJornada2()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var finJornada = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa, finPausa);

            Assert.Throws<InvalidOperationException>(
                () => new Jornada(inicioJornada, finJornada, pausa));
        }

        [Fact]
        public void NoSePuedeCrearJornadaTerminadaConPausasFueraDeLasFechasDeLaJornada3()
        {
            var fixture = new Fixture();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var finJornada = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa, finPausa);

            Assert.Throws<InvalidOperationException>(
                () => new Jornada(inicioJornada, finJornada, pausa));
        }

        [Fact]
        public void UnaJornadaIniciadaSinPausasNoTienePausaIniciada()
        {
            var fixture = new Fixture();
            var inicio = fixture.CreateAnonymous<DateTime>();

            var sut = new Jornada(inicio);

            sut.PausaIniciada.Should().BeNull();
        }

        [Fact]
        public void UnaJornadaIniciadaConUltimaPausaIniciadaTienePausaIniciada()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa);

            var sut = new Jornada(inicioJornada, pausa);

            sut.PausaIniciada.Should().NotBeNull();
        }

        [Fact]
        public void UnaJornadaIniciadaConUltimaPausaTerminadaNoTienePausaIniciada()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa, finPausa);

            var sut = new Jornada(inicioJornada, pausa);

            sut.PausaIniciada.Should().BeNull();
        }

        [Fact]
        public void UnaJornadaTerminadaNoPuedeIniciarPausa()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var finJornada = fixture.CreateAnonymous<DateTime>();

            var sut = new Jornada(inicioJornada, finJornada);

            Assert.Throws<InvalidOperationException>(() => sut.IniciarPausa(inicioPausa));
        }

        [Fact]
        public void UnaJornadaNoTerminadaNoPuedeIniciarPausaSiYaHayUnaPausaIniciada()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var inicioPausa1 = fixture.CreateAnonymous<DateTime>();
            var inicioPausa2 = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa1);

            var sut = new Jornada(inicioJornada, pausa);
            sut.PausaIniciada.Should().NotBeNull();

            Assert.Throws<InvalidOperationException>(
                () => sut.IniciarPausa(inicioPausa2));
        }

        [Fact]
        public void UnaJornadaNoTerminadaPuedeIniciarUnaPausa()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();

            var sut = new Jornada(inicioJornada);
            sut.PausaIniciada.Should().BeNull();

            sut.IniciarPausa(inicioPausa);

            sut.PausaIniciada.Should().NotBeNull();
        }

        [Fact]
        public void UnaJornadaNoTerminadaNoPuedeIniciarUnaPausaAntesDelInicioDeLaJornada()
        {
            var fixture = new Fixture();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();

            var sut = new Jornada(inicioJornada);
            sut.PausaIniciada.Should().BeNull();

            Assert.Throws<InvalidOperationException>(
                () => sut.IniciarPausa(inicioPausa));
        }

        [Fact]
        public void UnaJornadaTerminadaNoPuedeTerminarUnaPausa()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();
            var finJornada = fixture.CreateAnonymous<DateTime>();

            var sut = new Jornada(inicioJornada, finJornada);

            Assert.Throws<InvalidOperationException>(() => sut.TerminarPausa(finPausa));
        }

        [Fact]
        public void UnaJornadaNoTerminadaNoPuedeTerminarPausaSiNoHayUnaPausaIniciada()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();

            var sut = new Jornada(inicioJornada);
            sut.PausaIniciada.Should().BeNull();

            Assert.Throws<InvalidOperationException>(
                () => sut.TerminarPausa(finPausa));
        }

        [Fact]
        public void UnaJornadaNoTerminadaConPausaIniciadaPuedeTerminarUnaPausa()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa);

            var sut = new Jornada(inicioJornada, pausa);
            sut.PausaIniciada.Should().NotBeNull();

            sut.TerminarPausa(finPausa);

            sut.PausaIniciada.Should().BeNull();
        }

        [Fact]
        public void UnaJornadaNoTerminadaConPausaIniciadaNoPuedeTerminarPausaAntesDelInicioDeLaPausa()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa);

            var sut = new Jornada(inicioJornada, pausa);
            sut.PausaIniciada.Should().NotBeNull();

            Assert.Throws<InvalidOperationException>(
                () => sut.TerminarPausa(finPausa));
        }

        [Fact]
        public void UnaJornadaNoTerminadaSinPausasSePuedeTerminar()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var finJornada = fixture.CreateAnonymous<DateTime>();

            var sut = new Jornada(inicioJornada);

            sut.Finalizar(finJornada);

            sut.EstaIniciado.Should().BeFalse();
        }

        [Fact]
        public void UnaJornadaNoTerminadaConPausasTerminadasSePuedeTerminar()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var finPausa = fixture.CreateAnonymous<DateTime>();
            var finJornada = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa, finPausa);

            var sut = new Jornada(inicioJornada, pausa);

            sut.Finalizar(finJornada);

            sut.EstaIniciado.Should().BeFalse();
        }

        [Fact]
        public void UnaJornadaNoTerminadaConUnaOausaIniciadaNoSePuedeTerminar()
        {
            var fixture = new Fixture();
            var inicioJornada = fixture.CreateAnonymous<DateTime>();
            var inicioPausa = fixture.CreateAnonymous<DateTime>();
            var finJornada = fixture.CreateAnonymous<DateTime>();
            var pausa = new Pausa(inicioPausa);

            var sut = new Jornada(inicioJornada, pausa);

            Assert.Throws<InvalidOperationException>(
                () => sut.Finalizar(finJornada));
        }
    }
}
