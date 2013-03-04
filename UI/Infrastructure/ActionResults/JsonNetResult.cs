// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonNetResult.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the JsonNetResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Infrastructure.ActionResults
{
    using System;
    using System.Web.Mvc;

    using Acheve.UI.Infrastructure;

    using Newtonsoft.Json;

    public class JsonNetResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("No se permiten llamadas get sin autorización expresa");
            }

            var response = context.HttpContext.Response;

            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data == null)
            {
                return;
            }

            response.Write(JsonConvert.SerializeObject(this.Data, JsonSettings.FormattingSettings));
        }
    }
}