// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InformeResumenEntreFechasViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the InformeResumenEntreFechasViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Acheve.Domain.Entities;

    public class InformeResumenEntreFechasViewModel
    {
        public DateTime Inicio { get; set; }

        public DateTime Fin { get; set; }

        public IEnumerable<ResumenJornada> ResumenJornadas { get; set; }

        public bool IsPost { get; set; }
    }
}