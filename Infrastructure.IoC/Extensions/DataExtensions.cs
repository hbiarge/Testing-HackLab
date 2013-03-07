// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataExtensions.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the DataExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Infrastructure.IoC.Extensions
{
    using Acheve.Data.Services.Contracts;
    using Acheve.Data.Services.Contracts.Fakes;
    using Acheve.Data.Services.PetaPoco;

    using Microsoft.Practices.Unity;

    using PetaPoco;

    public class DataExtensions : UnityContainerExtension
    {
        protected override void Initialize()
        {
            //this.Container.RegisterType<IDatabase, Database>(
            //    new PerResolveLifetimeManager(),
            //    new InjectionConstructor("Presencia"));

            //this.Container.RegisterType<IJornadaQueries, PetaPocoJornadaQueries>(new PerResolveLifetimeManager());
            //this.Container.RegisterType<IJornadaCommands, PetaPocoJornadaCommands>(new PerResolveLifetimeManager());
            //this.Container.RegisterType<IUsuariosQueries, PetaPocoUsuariosQueries>(new PerResolveLifetimeManager());

            this.Container.RegisterType<IJornadaQueries, InMemoryJornadaRepository>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IJornadaCommands, InMemoryJornadaRepository>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IUsuariosQueries, InMemoryUsuariosRepositry>(new ContainerControlledLifetimeManager());
        }
    }
}
