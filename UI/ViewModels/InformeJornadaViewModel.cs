// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformeJornadaViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InformeJornadaViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Acheve.Domain.Entities;

    public class InformeJornadaViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public Jornada Jornada { get; set; }

        public TimeSpan SumaTrabajo { get; set; }

        public TimeSpan SumaPausa { get; set; }

        public TimeSpan Total { get; set; }

        public bool IsPost { get; set; }
    }
}