// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsuariosQueries.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the IUsuariosQueries type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Acheve.Data.Services.Contracts
{
    using System.Collections.Generic;

    public interface IUsuariosQueries
    {
        IEnumerable<string> ObtenerUsuarios();

        bool EsUsuarioValido(string nombre, string password);

        IEnumerable<string> ObtenerRolesDeUsuario(string username);
    }
}