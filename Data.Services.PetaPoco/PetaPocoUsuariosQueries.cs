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

    public class PetaPocoUsuariosQueries : IUsuariosQueries, IDisposable
    {
        private readonly IDatabase database;

        private bool disposed;

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

        public bool EsUsuarioValido(string nombre, string password)
        {
            var query =
                Sql.Builder
                .Append("SELECT Usuario, Password")
                .Append("FROM Usuarios")
                .Append("WHERE Usuario = @0 AND Password = @1", nombre, password);

            var usuario = this.database.FirstOrDefault<dynamic>(query);

            return usuario != null && usuario.Usuario == nombre && usuario.Password == password;
        }

        public IEnumerable<string> ObtenerRolesDeUsuario(string username)
        {
            var query =
                Sql.Builder
                .Append("SELECT Rol")
                .Append("FROM UsuariosRoles")
                .Append("WHERE Usuario = @0", username);

            return this.database.Query<string>(query);
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.database.Dispose();
            }
        }
    }
}
