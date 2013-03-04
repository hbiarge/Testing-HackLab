// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Periodo.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the Periodo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Entities
{
    using System;

    public class Periodo
    {
        private readonly DateTime inicio;

        private DateTime? fin;

        public Periodo(DateTime inicio)
        {
            this.inicio = inicio;
        }

        public Periodo(DateTime inicio, DateTime fin)
        {
            VerificarFinMayorInicio(inicio, fin);

            this.inicio = inicio;
            this.fin = fin;
        }

        public DateTime Inicio
        {
            get
            {
                return this.inicio;
            }
        }

        public DateTime? Fin
        {
            get
            {
                return this.fin;
            }
        }

        public TimeSpan Duracion
        {
            get
            {
                return this.fin.HasValue ?
                    this.fin.Value.Subtract(this.inicio) :
                    TimeSpan.Zero;
            }
        }

        public bool EstaIniciado
        {
            get
            {
                return this.fin.HasValue == false;
            }
        }

        public virtual void Finalizar(DateTime finalizacion)
        {
            if (this.EstaIniciado == false)
            {
                throw new InvalidOperationException("No se puede terminar un periodo ya terminado.");
            }

            VerificarFinMayorInicio(this.inicio, finalizacion);

            this.fin = finalizacion;
        }

        private static void VerificarFinMayorInicio(DateTime inicio, DateTime fin)
        {
            if (fin < inicio)
            {
                throw new InvalidOperationException("La fecha de finalización debe ser posterior a la fecha de inicio.");
            }
        }
    }
}