// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CriteriosBusquedaFechaViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the CriteriosBusquedaFechaViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CriteriosBusquedaFechaViewModel
    {
        public string Usuario { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }
}