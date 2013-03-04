// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequireRolePolicyViolationHandler.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the RequireRolePolicyViolationHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Infrastructure.FluentSecurityViolationHandlers
{
    using System.Web.Mvc;

    using FluentSecurity;

    public class RequireRolePolicyViolationHandler : IPolicyViolationHandler
    {
        public ActionResult Handle(PolicyViolationException exception)
        {
            return new ViewResult { ViewName = "NoAutorizado", MasterName = "_Layout" };
        }
    }
}