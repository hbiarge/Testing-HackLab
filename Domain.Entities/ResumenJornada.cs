// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResumenJornada.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the ResumenJornada type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Entities
{
    using System;

    public class ResumenJornada
    {
        public DateTime Dia { get; set; }

        public TimeSpan Trabajo { get; set; }

        public TimeSpan Pausa { get; set; }

        public TimeSpan Total { get; set; }
    }
}
