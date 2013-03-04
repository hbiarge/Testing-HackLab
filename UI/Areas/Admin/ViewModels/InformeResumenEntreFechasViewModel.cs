// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformeResumenEntreFechasViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InformeResumenEntreFechasViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Acheve.Domain.Entities;

    public class InformeResumenEntreFechasViewModel
    {
        public IEnumerable<string> Usuarios { get; set; }

        public string Usuario { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Inicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fin { get; set; }

        public IEnumerable<ResumenJornada> ResumenJornadas { get; set; }

        public bool IsPost { get; set; }
    }
}