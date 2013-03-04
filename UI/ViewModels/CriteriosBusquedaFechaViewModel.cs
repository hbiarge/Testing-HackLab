// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CriteriosBusquedaFechaViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the CriteriosBusquedaFechaViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CriteriosBusquedaFechaViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }
}