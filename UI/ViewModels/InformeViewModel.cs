// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformeViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InformeViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InformeViewModel
    {
        [DataType(DataType.Date)]
        public DateTime? Fecha { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Inicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Fin { get; set; }

        public string Error { get; set; }
    }
}