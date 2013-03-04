// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocaleDateTimeBinder.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the LocaleDateTimeBinder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Infrastructure
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;

    public class LocaleDateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var date = value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);

            return date;
        }
    }
}