// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PausaDbRow.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the PausaDbRow type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Data.Services.PetaPoco.Models
{
    using System;

    using global::PetaPoco;

    [TableName("Pausas")]
    internal class PausaDbRow
    {
        public int IdPausa { get; set; }

        public int IdJornada { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime? Fin { get; set; }

        [Ignore]
        public TimeSpan Duracion
        {
            get
            {
                return this.Fin.HasValue
                    ? this.Fin.Value.Subtract(this.Inicio)
                    : TimeSpan.Zero;
            }
        }
    }
}