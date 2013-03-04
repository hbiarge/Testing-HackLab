// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PetaPocoJornadasQueriesTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the PetaPocoJornadasQueriesTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco.IntegrationTests
{
    using System;
    using System.Linq;

    using Acheve.Data.Services.PetaPoco;

    using FluentAssertions;

    using global::PetaPoco;

    using Xunit;

    public class PetaPocoJornadasQueriesTests
    {
        private const string Usuario = "Hugo";

        [Fact]
        public void ObtenerUltimaJornada()
        {
            var database = new Database("Presencia");
            var sut = new PetaPocoJornadaQueries(database);

            var ultimaJornada = sut.ObtenerUltimaJornada(Usuario);

            ultimaJornada.Should().NotBeNull();
        }

        [Fact]
        public void ObtenerJornada()
        {
            var database = new Database("Presencia");
            var sut = new PetaPocoJornadaQueries(database);
            var fecha = new DateTime(2013, 2, 16);

            var ultimaJornada = sut.ObtenerJornada(Usuario, fecha);

            ultimaJornada.Should().NotBeNull();
        }

        [Fact]
        public void ObtenerDiasUltimasJornadasRegistradas()
        {
            var database = new Database("Presencia");
            var sut = new PetaPocoJornadaQueries(database);
            const int NumeroMaximoDias = 1;

            var ultimaJornada = sut.ObtenerDiasUltimasJornadasRegistradas(Usuario, NumeroMaximoDias);

            ultimaJornada.Should().HaveCount(1);
        }

        [Fact]
        public void ExisteJornadaTrue()
        {
            var database = new Database("Presencia");
            var sut = new PetaPocoJornadaQueries(database);
            var fecha = new DateTime(2013, 2, 16);

            var ultimaJornada = sut.ExisteJornada(Usuario, fecha);

            ultimaJornada.Should().BeTrue();
        }

        [Fact]
        public void ExisteJornadaFalse()
        {
            var database = new Database("Presencia");
            var sut = new PetaPocoJornadaQueries(database);
            var fecha = new DateTime(2013, 1, 1);

            var ultimaJornada = sut.ExisteJornada(Usuario, fecha);

            ultimaJornada.Should().BeFalse();
        }

        [Fact]
        public void ObtenerResumenEntreFechas()
        {
            var database = new Database("Presencia");
            var sut = new PetaPocoJornadaQueries(database);
            var inicio = new DateTime(2013, 2, 1);
            var fin = new DateTime(2013, 2, 20);

            var infoJornadas = sut.ObtenerResumenEntreFechas(Usuario, inicio, fin).ToArray();

            infoJornadas.Should().HaveCount(2);
        }

        [Fact]
        public void ObtenerInformacionJornadasEntreFechas()
        {
            var database = new Database("Presencia");
            var sut = new PetaPocoJornadaQueries(database);
            var inicio = new DateTime(2013, 2, 1);
            var fin = new DateTime(2013, 2, 20);

            var infoJornadas = sut.ObtenerInformacionJornadasEntreFechas(Usuario, inicio, fin).ToArray();

            infoJornadas.Should().HaveCount(20);
            infoJornadas.Where(ij => ij.Existe).Should().HaveCount(2);
        }
    }
}
