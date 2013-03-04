// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryJornadaRepository.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InMemoryJornadaRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Acheve.Data.Services.Contracts.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Acheve.Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class InMemoryJornadaRepository : IJornadaQueries, IJornadaCommands
    {
        private static readonly List<Jornada> Jornadas = new List<Jornada>();

        public void ActualizarJornada(Jornada jornada, string usuario)
        {
            var jornadaActual = Jornadas.FirstOrDefault(j => j.Id == jornada.Id);

            if (ReferenceEquals(jornada, jornadaActual) == false)
            {
                var index = Jornadas.IndexOf(jornadaActual);
                Jornadas.RemoveAt(index);
                Jornadas.Insert(index, jornada);
            }
        }

        public void CrearJornada(Jornada jornada, string usuario)
        {
            var maxId = 0;

            if (Jornadas.Any())
            {
                maxId = Jornadas.Max(j => j.Id);
            }

            jornada.Id = maxId + 1;
            Jornadas.Add(jornada);
        }

        public void EliminarJornada(int idJornada)
        {
            var jornada = Jornadas.FirstOrDefault(j => j.Id == idJornada);

            if (jornada != null)
            {
                Jornadas.Remove(jornada);
            }
        }

        public bool ExisteJornada(string usuario, DateTime diaNuevaJornada)
        {
            return Jornadas.Any(j => j.Dia.Date == diaNuevaJornada.Date);
        }

        public IEnumerable<DateTime> ObtenerDiasUltimasJornadasRegistradas(string usuario, int maximo)
        {
            return
                Jornadas.Where(j => j.EstaIniciado == false)
                        .OrderByDescending(j => j.Dia)
                        .Select(j => j.Dia)
                        .Take(maximo);
        }

        public IEnumerable<JornadaInfo> ObtenerInformacionJornadasEntreFechas(
            string usuario, DateTime inicio, DateTime fin)
        {
            var fechasConJornadas =
                new HashSet<DateTime>(
                    Jornadas.Where(j => j.Inicio.Date >= inicio.Date && j.Inicio.Date <= fin.Date)
                            .Select(j => j.Inicio.Date));

            for (var dia = inicio.Date; dia <= fin.Date; dia = dia.AddDays(1))
            {
                yield return new JornadaInfo { Dia = dia, Existe = fechasConJornadas.Contains(dia) };
            }
        }

        public Jornada ObtenerJornada(string name, DateTime fecha)
        {
            var jornada = Jornadas.FirstOrDefault(j => j.Inicio.Date == fecha.Date);

            return jornada ?? Jornada.NullJornada;
        }

        public IEnumerable<ResumenJornada> ObtenerResumenEntreFechas(string usuario, DateTime inicio, DateTime fin)
        {
            var jornadasjornadasEntreFechas =
                Jornadas.Where(j => j.Dia.Date >= inicio.Date && j.Dia.Date <= fin.Date && j.EstaIniciado == false);

            return from j in jornadasjornadasEntreFechas
                   let totalPausa = j.Pausas.Aggregate(TimeSpan.Zero, (span, pausa) => span.Add(pausa.Duracion))
                   select
                       new ResumenJornada
                       {
                           Dia = j.Dia,
                           Total = j.Duracion,
                           Pausa = totalPausa,
                           Trabajo = j.Duracion.Subtract(totalPausa)
                       };
        }

        public Jornada ObtenerUltimaJornada(string usuario)
        {
            var ultimaJornada = Jornadas.LastOrDefault();

            return ultimaJornada ?? Jornada.NullJornada;
        }
    }
}