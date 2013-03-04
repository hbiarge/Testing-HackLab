// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfrastructureExtensions.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the DataExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Infrastructure.IoC.Extensions
{
    using Acheve.Infrastructure.Services;
    using Acheve.Infrastructure.Services.Contracts;

    using Microsoft.Practices.Unity;

    public class InfrastructureExtensions : UnityContainerExtension
    {
        protected override void Initialize()
        {
            this.Container.RegisterType<ISettingsProvider, DefaultSettingsProvider>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<ITimeProvider, DefaultTimeProvider>(new ContainerControlledLifetimeManager());
        }
    }
}
