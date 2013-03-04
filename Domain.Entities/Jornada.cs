// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Jornada.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the Jornada type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Jornada : Periodo
    {
        private static readonly Jornada StaticNullJornada = new Jornada(DateTime.MinValue, DateTime.MinValue);

        private readonly List<Pausa> pausas = new List<Pausa>();

        public Jornada(DateTime inicio)
            : base(inicio)
        {
        }

        public Jornada(DateTime inicio, params Pausa[] pausas)
            : base(inicio)
        {
            if (pausas.Any(p => p.Inicio < inicio))
            {
                throw new InvalidOperationException("No se puede crear una Jornada no terminada con pausas que se inician antes de la fecha de inicio de la jornada.");
            }

            this.pausas.AddRange(pausas);
        }

        public Jornada(DateTime inicio, DateTime fin)
            : base(inicio, fin)
        {
        }

        public Jornada(DateTime inicio, DateTime fin, params Pausa[] pausas)
            : base(inicio, fin)
        {
            if (pausas.Any(p => p.Inicio < inicio || p.Fin > fin))
            {
                throw new InvalidOperationException("No se puede crear una Jornada terminada con pausas que se inician o terminan fuera del periodo comprendido entre el inicio y el fin de la jornada.");
            }

            this.pausas.AddRange(pausas);
        }

        public static Jornada NullJornada
        {
            get
            {
                return StaticNullJornada;
            }
        }

        public int Id { get; set; }

        public DateTime Dia
        {
            get
            {
                return this.Inicio.Date;
            }
        }

        public IEnumerable<Pausa> Pausas
        {
            get
            {
                return this.pausas.AsEnumerable();
            }
        }

        public Pausa PausaIniciada
        {
            get
            {
                var ultimaPausa = this.pausas
                    .OrderBy(p => p.Inicio)
                    .LastOrDefault(p => p.EstaIniciado);

                return ultimaPausa;
            }
        }

        public bool IsNull
        {
            get
            {
                return this.Inicio == DateTime.MinValue &&
                    this.Fin.HasValue &&
                    this.Fin.Value == DateTime.MinValue;
            }
        }

        public override void Finalizar(DateTime finJornada)
        {
            if (this.PausaIniciada != null)
            {
                throw new InvalidOperationException("No se puede terminar una jornada con una pausa iniciada.");
            }

            base.Finalizar(finJornada);
        }

        public void IniciarPausa(DateTime inicioPausa)
        {
            if (this.EstaIniciado == false)
            {
                throw new InvalidOperationException("No se puede iniciar una pausa en una Jornada finalizada.");
            }

            if (this.PausaIniciada != null)
            {
                throw new InvalidOperationException("No se puede iniciar una pausa cuando ya hay una pausa iniciada.");
            }

            if (inicioPausa < this.Inicio)
            {
                throw new InvalidOperationException("No se puede iniciar una pausa en una fecha anterior al inicio de la jornada.");
            }

            var pausa = new Pausa(inicioPausa);
            this.pausas.Add(pausa);
        }

        public void TerminarPausa(DateTime finPausa)
        {
            if (this.EstaIniciado == false)
            {
                throw new InvalidOperationException("No se puede terminar una pausa en una Jornada finalizada.");
            }

            if (this.PausaIniciada == null)
            {
                throw new InvalidOperationException("No se puede terminar una pausa cuando no hay una pausa iniciada.");
            }

            this.PausaIniciada.Finalizar(finPausa);
        }
    }
}
