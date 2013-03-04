// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaInfo.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Entities
{
    using System;

    public class JornadaInfo
    {
        public DateTime Dia { get; set; }

        public bool Existe { get; set; }
    }
}