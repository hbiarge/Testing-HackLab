// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITimeProvider.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the ITimeProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Infrastructure.Services.Contracts
{
    using System;

    public interface ITimeProvider
    {
        DateTime Now { get; }

        DateTime Today { get; }
    }
}
