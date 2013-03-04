// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PausaEditViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the PausaEditViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Areas.Admin.ViewModels
{
    using System;

    public class PausaEditViewModel
    {
        public int Id { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime? Fin { get; set; }
    }
}