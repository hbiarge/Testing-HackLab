// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingsProvider.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the ISettingsProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Infrastructure.Services.Contracts
{
    public interface ISettingsProvider
    {
        T GetValue<T>(string key);
    }
}