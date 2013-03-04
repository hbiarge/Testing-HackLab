// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PeriodoTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the PeriodoTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Entities.UnitTests
{
    using System;

    using Acheve.Domain.Entities;

    using FluentAssertions;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;

    using Xunit;
    using Xunit.Extensions;

    public class PeriodoTests
    {
        [Fact]
        public void UnPeriodoSinFinEstaIniciado()
        {
            var fixture = new Fixture();
            var inicio = fixture.CreateAnonymous<DateTime>();
            var sut = new Periodo(inicio);

            sut.Inicio.Should().Be(inicio);
            sut.EstaIniciado.Should().BeTrue();
        }

        [Fact]
        public void UnPeriodoConFinNoEstaIniciado()
        {
            var fixture = new Fixture();
            var inicio = fixture.CreateAnonymous<DateTime>();
            var fin = fixture.CreateAnonymous<DateTime>();
            var sut = new Periodo(inicio, fin);

            sut.Inicio.Should().Be(inicio);
            sut.Fin.Should().Be(fin);
            sut.EstaIniciado.Should().BeFalse();
        }

        [Fact]
        public void NoSePuedeCrearUnPeriodoConFinMenorQueInicio()
        {
            var fixture = new Fixture();
            var fin = fixture.CreateAnonymous<DateTime>();
            var inicio = fixture.CreateAnonymous<DateTime>();

            Assert.Throws<InvalidOperationException>(() => new Periodo(inicio, fin));
        }

        [Fact]
        public void UnPeriodoIniciadoSePuedeTerminar()
        {
            var fixture = new Fixture();
            var inicio = fixture.CreateAnonymous<DateTime>();
            var fin = fixture.CreateAnonymous<DateTime>();
            var sut = new Periodo(inicio);

            sut.Finalizar(fin);

            sut.Inicio.Should().Be(inicio);
            sut.Fin.Should().Be(fin);
            sut.EstaIniciado.Should().BeFalse();
        }

        [Fact]
        public void UnPeriodoFinalizadoNoSePuedeTerminar()
        {
            var fixture = new Fixture();
            var inicio = fixture.CreateAnonymous<DateTime>();
            var fin = fixture.CreateAnonymous<DateTime>();
            var sut = new Periodo(inicio, fin);

            Assert.Throws<InvalidOperationException>(() => sut.Finalizar(fin));
        }

        [Fact]
        public void UnPeriodoIniciadoNoSePuedeTerminarConFinMenorQueInicio()
        {
            var fixture = new Fixture();
            var fin = fixture.CreateAnonymous<DateTime>();
            var inicio = fixture.CreateAnonymous<DateTime>();
            var sut = new Periodo(inicio);

            Assert.Throws<InvalidOperationException>(() => sut.Finalizar(fin));
        }

        [Theory, AutoData]
        public void UnPeriodoIniciadoTieneDuracionZero(Periodo sut)
        {
            sut.Duracion.Should().Be(TimeSpan.Zero);
        }

        [Theory, AutoData]
        public void UnPeriodoTerminadoTieneDuracion(Periodo sut, DateTime fin)
        {
            sut.Finalizar(fin);
            var expected = fin.Subtract(sut.Inicio);

            sut.Duracion.Should().Be(expected);
        }
    }
}
