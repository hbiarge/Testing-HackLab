// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaDbRow.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaDbRow type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Acheve.Domain.Entities;

    using global::PetaPoco;

    [TableName("Jornadas")]
    internal class JornadaDbRow
    {
        public JornadaDbRow()
        {
            this.Pausas = new Collection<PausaDbRow>();
        }

        public int IdJornada { get; set; }

        public string Usuario { get; set; }

        public DateTime Fecha { get; set; }

        public DateTime Entrada { get; set; }

        public DateTime? Salida { get; set; }

        [Ignore]
        public ICollection<PausaDbRow> Pausas { get; set; }

        [Ignore]
        public TimeSpan Duracion
        {
            get
            {
                return this.Salida.HasValue
                       ? this.Salida.Value.Subtract(this.Entrada)
                       : TimeSpan.Zero;
            }
        }

        public static JornadaDbRow FromJornada(Jornada jornada, string usuario)
        {
            var jornadaDbRow = new JornadaDbRow
            {
                IdJornada = jornada.Id,
                Fecha = jornada.Dia,
                Entrada = jornada.Inicio,
                Salida = jornada.Fin,
                Usuario = usuario
            };

            foreach (var pausa in jornada.Pausas)
            {
                var pausaDbRow = new PausaDbRow
                {
                    IdPausa = pausa.Id,
                    IdJornada = jornada.Id,
                    Inicio = pausa.Inicio,
                    Fin = pausa.Fin
                };

                jornadaDbRow.Pausas.Add(pausaDbRow);
            }

            return jornadaDbRow;
        }

        public Jornada ToJornada()
        {
            var pausas = new List<Pausa>(this.Pausas.Count);

            foreach (var pausa in this.Pausas)
            {
                if (pausa.Fin.HasValue)
                {
                    pausas.Add(new Pausa(pausa.Inicio, pausa.Fin.Value) { Id = pausa.IdPausa });
                }
                else
                {
                    pausas.Add(new Pausa(pausa.Inicio) { Id = pausa.IdPausa });
                }
            }

            Jornada jornada;

            if (this.Salida.HasValue)
            {
                jornada = new Jornada(this.Entrada, this.Salida.Value, pausas.ToArray());
            }
            else
            {
                jornada = new Jornada(this.Entrada, pausas.ToArray());
            }

            jornada.Id = this.IdJornada;

            return jornada;
        }
    }
}
