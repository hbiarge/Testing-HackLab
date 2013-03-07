// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PetaPocoJornadasCommandsTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the PetaPocoJornadasCommandsTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco.IntegrationTests
{
    using System.Linq;

    using Acheve.Data.Services.PetaPoco;
    using Acheve.Data.Services.PetaPoco.Models;
    using Acheve.Domain.Entities;

    using FluentAssertions;

    using global::PetaPoco;

    using Ploeh.AutoFixture.Xunit;

    using Xunit.Extensions;

    public class PetaPocoJornadasCommandsTests
    {
        public PetaPocoJornadasCommandsTests()
        {
            DatabaseHelper.RestartTestDatabase();
        }

        [Theory, AutoData]
        public void CrearJornada(
            [Greedy]Jornada jornada)
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaCommands(database);

            sut.CrearJornada(jornada, DatabaseHelper.Usuario);

            var jornadaDbRow = database.FirstOrDefault<JornadaDbRow>(
                "Select * from Jornadas where IdJornada = @0", jornada.Id);

            jornadaDbRow.Should().NotBeNull();

            database.Execute("DELETE FROM Pausas WHERE IdJornada = @0", jornada.Id);
            database.Execute("DELETE FROM Jornadas WHERE IdJornada = @0", jornada.Id);
        }

        [Theory, AutoData]
        public void ActualizarJornada(
            [Greedy]Jornada jornada)
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaCommands(database);

            sut.CrearJornada(jornada, DatabaseHelper.Usuario);

            var jornadaDbRow = database.FirstOrDefault<JornadaDbRow>(
                "Select * from Jornadas where IdJornada = @0", jornada.Id);

            jornadaDbRow.Should().NotBeNull();

            // Modificamos
            var nuevaPausa = new Pausa(jornada.Fin.Value, jornada.Fin.Value.AddHours(2));
            var idJornada = jornada.Id;
            jornada = new Jornada(
                jornada.Inicio,
                jornada.Fin.Value.AddDays(1),
                jornada.Pausas.Skip(1).Union(new[] { nuevaPausa }).ToArray());
            jornada.Id = idJornada;

            sut.ActualizarJornada(jornada, DatabaseHelper.Usuario);

            jornadaDbRow = database.FirstOrDefault<JornadaDbRow>(
                "Select * from Jornadas where IdJornada = @0", jornada.Id);

            jornadaDbRow.Should().NotBeNull();

            database.Execute("DELETE FROM Pausas WHERE IdJornada = @0", jornada.Id);
            database.Execute("DELETE FROM Jornadas WHERE IdJornada = @0", jornada.Id);
        }

        [Theory, AutoData]
        public void EliminarJornada(
            [Greedy]Jornada jornada)
        {
            var database = new Database(DatabaseHelper.Database);
            var sut = new PetaPocoJornadaCommands(database);

            sut.CrearJornada(jornada, DatabaseHelper.Usuario);

            var jornadaDbRow = database.FirstOrDefault<JornadaDbRow>(
                "SELECT * FROM Jornadas WHERE IdJornada = @0", jornada.Id);

            jornadaDbRow.Should().NotBeNull();

            sut.EliminarJornada(jornada.Id);

            jornadaDbRow = database.FirstOrDefault<JornadaDbRow>(
                "SELECT * FROM Jornadas WHERE IdJornada = @0", jornada.Id);

            jornadaDbRow.Should().BeNull();
        }
    }
}
