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
        public PetaPocoJornadasQueriesTests()
        {
            DatabaseHelper.RestartTestDatabase();
        }

        [Fact]
        public void ObtenerUltimaJornada()
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaQueries(database);

            var ultimaJornada = sut.ObtenerUltimaJornada(DatabaseHelper.Usuario);

            ultimaJornada.IsNull.Should().BeFalse();
        }

        [Fact]
        public void ObtenerJornada()
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaQueries(database);
            var fecha = DateTime.Today.AddDays(-1);

            var ultimaJornada = sut.ObtenerJornada(DatabaseHelper.Usuario, fecha);

            ultimaJornada.IsNull.Should().BeFalse();
        }

        [Fact]
        public void ObtenerDiasUltimasJornadasRegistradas()
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaQueries(database);
            const int NumeroMaximoDias = 1;

            var ultimaJornada = sut.ObtenerDiasUltimasJornadasRegistradas(DatabaseHelper.Usuario, NumeroMaximoDias);

            ultimaJornada.Should().HaveCount(1);
        }

        [Fact]
        public void ExisteJornadaTrue()
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaQueries(database);
            var fecha = DateTime.Today.AddDays(-1);

            var ultimaJornada = sut.ExisteJornada(DatabaseHelper.Usuario, fecha);

            ultimaJornada.Should().BeTrue();
        }

        [Fact]
        public void ExisteJornadaFalse()
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaQueries(database);
            var fecha = DateTime.Today;

            var ultimaJornada = sut.ExisteJornada(DatabaseHelper.Usuario, fecha);

            ultimaJornada.Should().BeFalse();
        }

        [Fact]
        public void ObtenerResumenEntreFechas()
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaQueries(database);
            var inicio = DateTime.Today.AddDays(-1);
            var fin = DateTime.Today.AddDays(-1);

            var infoJornadas = sut.ObtenerResumenEntreFechas(DatabaseHelper.Usuario, inicio, fin).ToArray();

            infoJornadas.Should().HaveCount(1);
        }

        [Fact]
        public void ObtenerInformacionJornadasEntreFechas()
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaQueries(database);
            var inicio = DateTime.Today.AddDays(-19);
            var fin = DateTime.Today;

            var infoJornadas = sut.ObtenerInformacionJornadasEntreFechas(DatabaseHelper.Usuario, inicio, fin).ToArray();

            infoJornadas.Should().HaveCount(20);
            infoJornadas.Where(ij => ij.Existe).Should().HaveCount(DatabaseHelper.NumeroDiasCreados);
        }
    }
}
