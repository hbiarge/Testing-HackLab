// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultSettingsProvider.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the DefaultSettingsProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Infrastructure.Services
{
    using System;
    using System.Configuration;
    using System.Globalization;

    using Acheve.Infrastructure.Services.Contracts;

    public class DefaultSettingsProvider : ISettingsProvider
    {
        public T GetValue<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }

            var value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException(string.Format("Missing [{0}] from .config file AppSettings section", key));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}