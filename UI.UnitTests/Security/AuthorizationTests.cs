// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the AuthorizationTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.Security
{
    using System.Linq;

    using Acheve.UI.App_Start;
    using Acheve.UI.Areas.Admin.Controllers;
    using Acheve.UI.Areas.Admin.ViewModels;
    using Acheve.UI.Controllers;
    using Acheve.UI.UnitTests.AutoFixture;

    using FluentAssertions;

    using FluentSecurity;
    using FluentSecurity.Policy;
    using FluentSecurity.TestHelper;

    using Microsoft.Practices.Unity;

    using Moq;

    using Ploeh.AutoFixture.Xunit;

    using Xunit.Extensions;

    public class AuthorizationTests
    {
        [Theory, AutoHttpData]
        public void AuthorizationIsConfigured([Frozen]Mock<IUnityContainer> stubContainer)
        {
            // Arrange
            SecurityConfig.Configure(stubContainer.Object);

            // Act
            var results = SecurityConfiguration.Current.Verify(expectations =>
            {
                // Cuenta controller
                expectations.Expect<CuentaController>(x => x.LogOn())
                    .Has<IgnorePolicy>();
                expectations.Expect<CuentaController>(x => x.LogOff())
                    .Has<DenyAnonymousAccessPolicy>();

                // Situacion controller
                expectations.Expect<SituacionController>(x => x.Actual())
                    .Has<DenyAnonymousAccessPolicy>();
                expectations.Expect<SituacionController>(x => x.UltimasJornadas())
                    .Has<DenyAnonymousAccessPolicy>();
                expectations.Expect<SituacionController>(x => x.IniciarJornada())
                    .Has<DenyAnonymousAccessPolicy>();
                expectations.Expect<SituacionController>(x => x.TerminarJornada())
                    .Has<DenyAnonymousAccessPolicy>();
                expectations.Expect<SituacionController>(x => x.IniciarPausa())
                    .Has<DenyAnonymousAccessPolicy>();
                expectations.Expect<SituacionController>(x => x.TerminarPausa())
                    .Has<DenyAnonymousAccessPolicy>();

                // Informe controller
                expectations.Expect<Acheve.UI.Controllers.InformeController>(x => x.Dia())
                    .Has<DenyAnonymousAccessPolicy>();
                expectations.Expect<Acheve.UI.Controllers.InformeController>(x => x.EntreFechas())
                    .Has<DenyAnonymousAccessPolicy>();

                //// Área de administración

                // Buscar controller
                expectations.Expect<BuscarController>(x => x.Index())
                    .Has<RequireAnyRolePolicy>(p => p.RolesRequired.Contains(SecurityConfig.Roles.RecurosHumanos));

                // Informe controller
                expectations.Expect<Acheve.UI.Areas.Admin.Controllers.InformeController>(x => x.Dia())
                    .Has<RequireAnyRolePolicy>(p => p.RolesRequired.Contains(SecurityConfig.Roles.RecurosHumanos));
                expectations.Expect<Acheve.UI.Areas.Admin.Controllers.InformeController>(x => x.EntreFechas())
                    .Has<RequireAnyRolePolicy>(p => p.RolesRequired.Contains(SecurityConfig.Roles.RecurosHumanos));

                // Jornada controller
                expectations.Expect<JornadaController>(x => x.Editar(new CriteriosBusquedaFechaViewModel()))
                    .Has<RequireAnyRolePolicy>(p => p.RolesRequired.Contains(SecurityConfig.Roles.RecurosHumanos));
                expectations.Expect<JornadaController>(x => x.Crear(new CriteriosBusquedaFechaViewModel()))
                    .Has<RequireAnyRolePolicy>(p => p.RolesRequired.Contains(SecurityConfig.Roles.RecurosHumanos));
                expectations.Expect<JornadaController>(x => x.Eliminar(new CriteriosBusquedaFechaViewModel()))
                    .Has<RequireAnyRolePolicy>(p => p.RolesRequired.Contains(SecurityConfig.Roles.RecurosHumanos));
            }).ToArray();

            // Assert
            results.ErrorMessages().Should().BeNullOrEmpty();
        }
    }
}
