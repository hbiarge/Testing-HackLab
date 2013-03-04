// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultTimeProvider.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the DefaultTimeProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Infrastructure.Services
{
    using System;

    using Acheve.Infrastructure.Services.Contracts;

    public class DefaultTimeProvider : ITimeProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public DateTime Today
        {
            get
            {
                return DateTime.Today;
            }
        }
    }
}
