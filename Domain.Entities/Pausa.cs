// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pausa.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the Pausa type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Domain.Entities
{
    using System;

    public class Pausa : Periodo
    {
        public const int IdNuevo = -1;

        public Pausa(DateTime inicio)
            : base(inicio)
        {
            this.Id = IdNuevo;
        }

        public Pausa(DateTime inicio, DateTime fin)
            : base(inicio, fin)
        {
            this.Id = IdNuevo;
        }

        public int Id { get; set; }
    }
}