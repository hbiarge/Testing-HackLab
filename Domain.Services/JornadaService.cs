// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaService.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Services
{
    using System;

    using Acheve.Data.Services.Contracts;
    using Acheve.Domain.Entities;
    using Acheve.Domain.Services.Contracts;
    using Acheve.Infrastructure.Services.Contracts;

    public class JornadaService : IJornadaService
    {
        private readonly IJornadaQueries jornadaQueries;

        private readonly IJornadaCommands jornadaCommands;

        private readonly ITimeProvider timeProvider;

        public JornadaService(
            IJornadaQueries jornadaQueries,
            IJornadaCommands jornadaCommands,
            ITimeProvider timeProvider)
        {
            if (jornadaQueries == null)
            {
                throw new ArgumentNullException("jornadaQueries");
            }

            if (jornadaCommands == null)
            {
                throw new ArgumentNullException("jornadaCommands");
            }

            if (timeProvider == null)
            {
                throw new ArgumentNullException("timeProvider");
            }

            this.jornadaQueries = jornadaQueries;
            this.jornadaCommands = jornadaCommands;
            this.timeProvider = timeProvider;
        }

        public void IniciarJornada(string usuario)
        {
            var ultimaJornada = this.jornadaQueries.ObtenerUltimaJornada(usuario);

            if (ultimaJornada.EstaIniciado)
            {
                throw new InvalidOperationException("Ya existe una jornada iniciada. Debe terminar la jornada iniciada antes de iniciar una nueva.");
            }

            var existeJornadaParaDiaNuevaJornada = this.jornadaQueries.ExisteJornada(usuario, this.timeProvider.Today);

            if (existeJornadaParaDiaNuevaJornada)
            {
                throw new InvalidOperationException("Ya existe una jornada para la fecha indicada.");
            }

            var nuevaJornada = new Jornada(this.timeProvider.Now);
            this.jornadaCommands.CrearJornada(nuevaJornada, usuario);
        }

        public void TerminarJornada(string usuario)
        {
            var ultimaJornada = this.jornadaQueries.ObtenerUltimaJornada(usuario);
            ultimaJornada.Finalizar(this.timeProvider.Now);
            this.jornadaCommands.ActualizarJornada(ultimaJornada, usuario);
        }

        public void IniciarPausa(string usuario)
        {
            var ultimaJornada = this.jornadaQueries.ObtenerUltimaJornada(usuario);
            ultimaJornada.IniciarPausa(this.timeProvider.Now);
            this.jornadaCommands.ActualizarJornada(ultimaJornada, usuario);
        }

        public void TerminarPausa(string usuario)
        {
            var ultimaJornada = this.jornadaQueries.ObtenerUltimaJornada(usuario);
            ultimaJornada.TerminarPausa(this.timeProvider.Now);
            this.jornadaCommands.ActualizarJornada(ultimaJornada, usuario);
        }
    }
}
