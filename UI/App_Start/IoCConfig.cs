// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoCConfig.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the IoCConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.App_Start
{
    using System.Web.Mvc;

    using Acheve.Infrastructure.IoC;
    using Acheve.UI.Infrastructure.FluentSecurityViolationHandlers;

    using FluentSecurity;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using Unity.Mvc4;

    public static class IoCConfig
    {
        public static IUnityContainer Configure()
        {
            var container = new UnityContainer();

            // Registramos los handlers de autorización
            container.RegisterType<IPolicyViolationHandler, DenyAnonymousAccessPolicyViolationHandler>("DenyAnonymousAccess");
            container.RegisterType<IPolicyViolationHandler, RequireRolePolicyViolationHandler>("RequireRole");

            IoC.Configure(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

            return container;
        }
    }
}