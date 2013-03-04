// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.ViewModels
{
    using Acheve.Domain.Entities;

    public class JornadaViewModel
    {
        public Jornada Jornada { get; set; }

        public string Error { get; set; }
    }
}