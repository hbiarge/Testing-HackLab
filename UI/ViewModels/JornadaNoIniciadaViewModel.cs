// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JornadaNoIniciadaViewModel.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JornadaNoIniciadaViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class JornadaNoIniciadaViewModel
    {
        private readonly IEnumerable<DateTime> ultimasJornadasRegistradas;

        public JornadaNoIniciadaViewModel(IEnumerable<DateTime> ultimasJornadasRegistradas)
        {
            this.ultimasJornadasRegistradas = ultimasJornadasRegistradas.ToArray();
        }

        public IEnumerable<DateTime> UltimasJornadasRegistradas
        {
            get
            {
                return this.ultimasJornadasRegistradas;
            }
        }
    }
}