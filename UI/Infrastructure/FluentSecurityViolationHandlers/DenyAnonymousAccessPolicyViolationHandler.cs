// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DenyAnonymousAccessPolicyViolationHandler.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the DenyAnonymousAccessPolicyViolationHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Infrastructure.FluentSecurityViolationHandlers
{
    using System.Web.Mvc;

    using FluentSecurity;

    public class DenyAnonymousAccessPolicyViolationHandler : IPolicyViolationHandler
    {
        public ActionResult Handle(PolicyViolationException exception)
        {
            return new HttpUnauthorizedResult(exception.Message);
        }
    }
}