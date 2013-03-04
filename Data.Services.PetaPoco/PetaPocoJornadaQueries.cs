// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PetaPocoJornadaQueries.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the PetaPocoJornadaQueries type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acheve.Data.Services.Contracts;
    using Acheve.Data.Services.PetaPoco.Models;
    using Acheve.Domain.Entities;

    using global::PetaPoco;

    public class PetaPocoJornadaQueries : IJornadaQueries
    {
        private readonly IDatabase database;

        public PetaPocoJornadaQueries(IDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            this.database = database;
        }

        public Jornada ObtenerUltimaJornada(string usuario)
        {
            var query = Sql.Builder
                .Append("SELECT * ")
                .Append("FROM Jornadas J")
                .Append("LEFT JOIN Pausas P ON P.IdJornada = J.IdJornada")
                .Append("WHERE J.Fecha = (SELECT MAX(Fecha) ")
                .Append("                 FROM Jornadas")
                .Append("                 WHERE Usuario = @0)", usuario);

            var jornadaDbRow = this.database
                .Fetch<JornadaDbRow, PausaDbRow, JornadaDbRow>(
                new JornadaDbRowPausaDbRowRelator().MapIt,
                query)
                .SingleOrDefault();

            return jornadaDbRow == null ? Jornada.NullJornada : jornadaDbRow.ToJornada();
        }

        public Jornada ObtenerJornada(string usuario, DateTime fecha)
        {
            var query = Sql.Builder
                .Append("SELECT * ")
                .Append("FROM Jornadas J")
                .Append("LEFT JOIN Pausas P ON P.IdJornada = J.IdJornada")
                .Append("WHERE  J.Usuario = @0 AND J.Fecha = @1", usuario, fecha.Date);

            var jornadaDbRow = this.database
                .Fetch<JornadaDbRow, PausaDbRow, JornadaDbRow>(
                new JornadaDbRowPausaDbRowRelator().MapIt,
                query)
                .SingleOrDefault();

            return jornadaDbRow == null ? Jornada.NullJornada : jornadaDbRow.ToJornada();
        }

        public IEnumerable<DateTime> ObtenerDiasUltimasJornadasRegistradas(string usuario, int numeroMaximoJornadas = 5)
        {
            var query = Sql.Builder
                .Append("SELECT Fecha")
                .Append("FROM Jornadas")
                .Append("WHERE Usuario = @0", usuario)
                .Append("ORDER BY Fecha DESC OFFSET 0 ROWS FETCH NEXT @0 ROWS ONLY", numeroMaximoJornadas);

            var dias = this.database.Query<DateTime>(query);

            return dias;
        }

        public bool ExisteJornada(string usuario, DateTime fecha)
        {
            var query = Sql.Builder
                .Append("SELECT * ")
                .Append("FROM Jornadas J")
                .Append("WHERE  J.Usuario = @0 AND J.Fecha = @1", usuario, fecha.Date);

            var jornadaDbRow = this.database.FirstOrDefault<JornadaDbRow>(query);

            return jornadaDbRow != null && (jornadaDbRow.Usuario == usuario && jornadaDbRow.Fecha.Date == fecha.Date);
        }

        public IEnumerable<ResumenJornada> ObtenerResumenEntreFechas(string usuario, DateTime inicio, DateTime fin)
        {
            var query = Sql.Builder
                .Append("SELECT * ")
                .Append("FROM Jornadas J")
                .Append("LEFT JOIN Pausas P ON P.IdJornada = J.IdJornada")
                .Append("WHERE  J.Usuario = @0 AND J.Fecha BETWEEN @1 AND @2", usuario, inicio.Date, fin.Date);

            var jornadasEntreFechas = this.database
                .Fetch<JornadaDbRow, PausaDbRow, JornadaDbRow>(
                new JornadaDbRowPausaDbRowRelator().MapIt,
                query);

            return from j in jornadasEntreFechas
                   let totalPausa = j.Pausas.Aggregate(TimeSpan.Zero, (span, pausa) => span.Add(pausa.Duracion))
                   select
                       new ResumenJornada
                       {
                           Dia = j.Fecha,
                           Total = j.Duracion,
                           Pausa = totalPausa,
                           Trabajo = j.Duracion.Subtract(totalPausa)
                       };
        }

        public IEnumerable<JornadaInfo> ObtenerInformacionJornadasEntreFechas(string usuario, DateTime inicio, DateTime fin)
        {
            var query = Sql.Builder
                .Append("SELECT Fecha")
                .Append("FROM Jornadas")
                .Append("WHERE Usuario = @0 AND Fecha BETWEEN @1 AND @2", usuario, inicio.Date, fin.Date)
                .Append("ORDER BY Fecha");

            var jornadasEnElRangoDeFechas = new HashSet<DateTime>(this.database.Query<DateTime>(query));

            for (var dia = inicio.Date; dia <= fin.Date; dia = dia.AddDays(1))
            {
                yield return new JornadaInfo { Dia = dia, Existe = jornadasEnElRangoDeFechas.Contains(dia) };
            }
        }
    }
}