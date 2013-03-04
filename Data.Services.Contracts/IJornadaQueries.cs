// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJornadaQueries.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the IJornadaQueries type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Acheve.Data.Services.Contracts
{
    using System;
    using System.Collections.Generic;

    using Acheve.Domain.Entities;

    public interface IJornadaQueries
    {
        bool ExisteJornada(string usuario, DateTime fecha);

        IEnumerable<DateTime> ObtenerDiasUltimasJornadasRegistradas(string usuario, int numeroMaximoJornadas = 5);

        IEnumerable<JornadaInfo> ObtenerInformacionJornadasEntreFechas(string usuario, DateTime inicio, DateTime fin);

        Jornada ObtenerJornada(string usuario, DateTime fecha);

        IEnumerable<ResumenJornada> ObtenerResumenEntreFechas(string usuario, DateTime inicio, DateTime fin);

        Jornada ObtenerUltimaJornada(string usuario);
    }
}