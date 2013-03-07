// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleHelper.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the RoleHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Infrastructure.Security
{
    using System.Collections.Generic;

    using Acheve.Data.Services.Contracts;

    using Microsoft.Practices.ServiceLocation;

    public static class RoleHelper
    {
        public static IEnumerable<string> GetRolesForUser(string username)
        {
            var queries = ServiceLocator.Current.GetInstance<IUsuariosQueries>();
            return queries.ObtenerRolesDeUsuario(username);
        }
    }
}