// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CriteriosBusquedaEntreFechasViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the CriteriosBusquedaEntreFechasViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Acheve.UI.Infrastructure;

    public class CriteriosBusquedaEntreFechasViewModel
    {
        [DataType(DataType.Date)]
        [MinorThan("Fin")]
        public DateTime Inicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fin { get; set; }
    }
}