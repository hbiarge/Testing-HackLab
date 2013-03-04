// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PetaPocoUsuariosQueries.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the PetaPocoUsuariosQueries type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco
{
    using System;
    using System.Collections.Generic;

    using Acheve.Data.Services.Contracts;

    using global::PetaPoco;

    public class PetaPocoUsuariosQueries : IUsuariosQueries
    {
        private readonly IDatabase database;

        public PetaPocoUsuariosQueries(IDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this.database = database;
        }

        public IEnumerable<string> ObtenerUsuarios()
        {
            var query = Sql.Builder
                .Append("SELECT DISTINCT Usuario")
                .Append("FROM Jornadas")
                .Append("ORDER BY Usuario");

            var usuarios = this.database.Query<string>(query);
            return usuarios;
        }
    }
}
