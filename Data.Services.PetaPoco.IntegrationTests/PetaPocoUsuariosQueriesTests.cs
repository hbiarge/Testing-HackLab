// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PetaPocoUsuariosQueriesTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the PetaPocoUsuariosQueriesTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco.IntegrationTests
{
    using Acheve.Data.Services.PetaPoco;

    using FluentAssertions;

    using global::PetaPoco;

    using Xunit;

    public class PetaPocoUsuariosQueriesTests
    {
        [Fact]
        public void ObtenerUsuariosRecuperaLosUsuariosUnicos()
        {
            var database = new Database("Presencia");
            var sut = new PetaPocoUsuariosQueries(database);

            var usuarios = sut.ObtenerUsuarios();

            usuarios.Should().Contain("Hugo");
        }
    }
}
