// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the IoC type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Infrastructure.IoC
{
    using Acheve.Infrastructure.IoC.Extensions;

    using Microsoft.Practices.Unity;

    public static class IoC
    {
        public static void Configure(IUnityContainer container)
        {
            container.AddNewExtension<DataExtensions>();
            container.AddNewExtension<InfrastructureExtensions>();
            container.AddNewExtension<ServicesExtensions>();
        }
    }
}
