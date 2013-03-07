namespace Acheve.UI.App_Start
{
    using System.Web;
    using System.Web.Mvc;

    using Acheve.UI.Areas.Admin.Controllers;
    using Acheve.UI.Controllers;
    using Acheve.UI.Infrastructure.Security;

    using FluentSecurity;
    using FluentSecurity.Caching;

    using Microsoft.Practices.Unity;

    public static class SecurityConfig
    {
        public static void Configure(IUnityContainer container)
        {
            SecurityConfigurator.Configure(configuration =>
            {
                configuration.ResolveServicesUsing(type => container.ResolveAll(type));

                // Configuración real
                configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                configuration.GetRolesFrom(() => RoleHelper.GetRolesForUser(HttpContext.Current.User.Identity.Name));

                configuration.Advanced.SetDefaultResultsCacheLifecycle(Cache.PerHttpSession);

                // Configuración de las páginas de usuario
                configuration.For<CuentaController>(x => x.LogOn())
                    .Ignore();
                configuration.For<CuentaController>(x => x.LogOff())
                    .DenyAnonymousAccess();
                configuration.For<Controllers.InformeController>()
                    .DenyAnonymousAccess();
                configuration.For<SituacionController>()
                    .DenyAnonymousAccess();

                // Configuración de las páginas de administración
                configuration.ForAllControllersInNamespaceContainingType<BuscarController>()
                    .RequireAnyRole(Roles.Jefe);
            });

            GlobalFilters.Filters.Add(new HandleSecurityAttribute(), 0);
        }

        public static class Roles
        {
            public const string Jefe = "JEFE";
        }
    }
}