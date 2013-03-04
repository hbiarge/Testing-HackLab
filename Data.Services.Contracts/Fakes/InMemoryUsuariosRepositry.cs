// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryUsuariosRepositry.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InMemoryUsuariosRepositry type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Acheve.Data.Services.Contracts.Fakes
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class InMemoryUsuariosRepositry : IUsuariosQueries
    {
        private static readonly List<string> Usuarios = new List<string>
        {
            "Usuario 1", 
            "Usuario 2", 
            "Usuario 3"
        };

        public IEnumerable<string> ObtenerUsuarios()
        {
            return Usuarios;
        }
    }
}