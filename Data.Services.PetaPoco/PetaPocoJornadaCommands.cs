// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PetaPocoJornadaCommands.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the PetaPocoJornadaCommands type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco
{
    using System;
    using System.Linq;

    using Acheve.Data.Services.Contracts;
    using Acheve.Data.Services.PetaPoco.Models;
    using Acheve.Domain.Entities;

    using global::PetaPoco;

    public class PetaPocoJornadaCommands : IJornadaCommands
    {
        private readonly IDatabase database;

        private bool disposed;

        public PetaPocoJornadaCommands(IDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this.database = database;
        }

        public void CrearJornada(Jornada jornada, string usuario)
        {
            var jornadaDbRow = JornadaDbRow.FromJornada(jornada, usuario);

            using (var transaction = this.database.GetTransaction())
            {
                this.database.Insert("Jornadas", "IdJornada", true, jornadaDbRow);
                jornada.Id = jornadaDbRow.IdJornada;

                var pausas = jornada.Pausas.Zip(jornadaDbRow.Pausas, (p, pdb) => new { Pausa = p, PausaDbRow = pdb });

                foreach (var pausa in pausas)
                {
                    pausa.PausaDbRow.IdJornada = jornadaDbRow.IdJornada;
                    this.database.Insert("Pausas", "IdPausa", true, pausa.PausaDbRow);
                    pausa.Pausa.Id = pausa.PausaDbRow.IdPausa;
                }

                transaction.Complete();
            }
        }

        public void ActualizarJornada(Jornada jornada, string usuario)
        {
            var jornadaDbRowActual = JornadaDbRow.FromJornada(jornada, usuario);

            // Obtener los datos de la jornada existente
            var query = Sql.Builder
                .Append("SELECT * ")
                .Append("FROM Jornadas J")
                .Append("LEFT JOIN Pausas P ON P.IdJornada = J.IdJornada")
                .Append("WHERE  J.IdJornada = @0", jornada.Id);

            var jornadaDbRowExistente = this.database
                .Fetch<JornadaDbRow, PausaDbRow, JornadaDbRow>(
                new JornadaDbRowPausaDbRowRelator().MapIt,
                query)
                .SingleOrDefault();

            if (jornadaDbRowExistente == null)
            {
                throw new InvalidOperationException("La jornada que intenta actualizar no existe. IdJornada = " + jornada.Id);
            }

            using (var transaction = this.database.GetTransaction())
            {
                this.database.Update("Jornadas", "IdJornada", jornadaDbRowActual);

                var pausasParaEliminar = jornadaDbRowExistente.Pausas.Select(p => p.IdPausa)
                                         .Except(jornadaDbRowActual.Pausas.Select(p => p.IdPausa));

                foreach (var pausaNuevaActualizar in jornadaDbRowActual.Pausas)
                {
                    if (pausaNuevaActualizar.IdPausa == -1)
                    {
                        this.database.Insert("Pausas", "IdPausa", true, pausaNuevaActualizar);
                    }
                    else
                    {
                        this.database.Update("Pausas", "IdPausa", pausaNuevaActualizar);
                    }
                }

                foreach (var idPausaEliminar in pausasParaEliminar)
                {
                    this.database.Execute("DELETE FROM Pausas WHERE IdPausa = @0", idPausaEliminar);
                }

                transaction.Complete();
            }
        }

        public void EliminarJornada(int idJornada)
        {
            using (var transaction = this.database.GetTransaction())
            {
                this.database.Execute("DELETE FROM Pausas WHERE IdJornada = @0", idJornada);
                this.database.Execute("DELETE FROM Jornadas WHERE IdJornada = @0", idJornada);

                transaction.Complete();
            }
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