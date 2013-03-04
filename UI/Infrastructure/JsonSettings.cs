// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonSettings.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JsonSettings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Infrastructure
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class JsonSettings
    {
        public static readonly JsonSerializerSettings FormattingSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Include,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };
    }
}