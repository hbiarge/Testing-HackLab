// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelStateExtensions.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the ModelStateExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public static class ModelStateExtensions
    {
        public static IEnumerable<Error> GetValidationErrors(this ModelStateDictionary modelState)
        {
            return modelState
                .Where(x => x.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors, (m, e) => new Error { Key = m.Key, ErrorMessage = e.ErrorMessage });
        }
    }
}