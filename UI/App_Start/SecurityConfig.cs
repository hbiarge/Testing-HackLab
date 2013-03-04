namespace Acheve.UI.App_Start
{
    using System.Web;
    using System.Web.Mvc;

    using Acheve.UI.Areas.Admin.Controllers;
    using Acheve.UI.Controllers;
    
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
                //configuration.GetRolesFrom(() => ActiveDirectoryRoleHelper.GetUserRoles(HttpContext.Current.User.Identity.Name));

                configuration.Advanced.SetDefaultResultsCacheLifecycle(Cache.PerHttpSession);

                // Configuración de las páginas de usuario
                configuration.For<CuentaController>(x => x.LogOn())
                    .Ignore();
                configuration.For<CuentaController>(x => x.LogOff())
                    .DenyAnonymousAccess();
                configuration.For<global::Acheve.UI.Controllers.InformeController>()
                    .DenyAnonymousAccess();
                configuration.For<SituacionController>()
                    .DenyAnonymousAccess();

                // Configuración de las páginas de administración
                configuration.ForAllControllersInNamespaceContainingType<BuscarController>()
                    .RequireAnyRole(Roles.RecurosHumanos);
            });

            GlobalFilters.Filters.Add(new HandleSecurityAttribute(), 0);
        }

        public static class Roles
        {
            public const string RecurosHumanos = "RRHH1";
        }
    }
}