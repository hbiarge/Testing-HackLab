// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesExtensions.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the ServicesExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Infrastructure.IoC.Extensions
{
    using Acheve.Domain.Services;
    using Acheve.Domain.Services.Contracts;

    using Microsoft.Practices.Unity;

    public class ServicesExtensions : UnityContainerExtension
    {
        protected override void Initialize()
        {
            this.Container.RegisterType<IJornadaService, JornadaService>();
        }
    }
}