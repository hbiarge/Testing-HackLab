// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuscarJornadasViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the BuscarJornadasViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Acheve.Domain.Entities;

    public class BuscarJornadasViewModel
    {
        public IEnumerable<string> Usuarios { get; set; }

        public string Usuario { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Inicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Fin { get; set; }

        public IEnumerable<JornadaInfo> Jornadas { get; set; }

        public bool IsPost { get; set; }
    }
}