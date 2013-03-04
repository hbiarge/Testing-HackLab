// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpMocksCustomization.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the HttpMocksCustomization type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.AutoFixture
{
    using System.Security.Principal;
    using System.Web;

    using Moq;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    public class HttpMocksCustomization : CompositeCustomization
    {
        public HttpMocksCustomization()
            : base(
            new HttpRequestBaseMockCustomization(),
            new HttpResponseBaseMockCustomization(),
            new IdentityMockCustomization(),
            new PrincipalMockCustomization(),
            new HttpContextBaseMockCustomization(),
            new AutoMoqCustomization())
        {
        }

        private class HttpContextBaseMockCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var requestMock = fixture.CreateAnonymous<Mock<HttpRequestBase>>();
                var responseMock = fixture.CreateAnonymous<Mock<HttpResponseBase>>();
                var userMock = fixture.CreateAnonymous<Mock<IPrincipal>>();
                var mock = new Mock<HttpContextBase>(MockBehavior.Loose);
                mock.Setup(m => m.Request).Returns(requestMock.Object);
                mock.Setup(m => m.Response).Returns(responseMock.Object);
                mock.Setup(m => m.User).Returns(userMock.Object);

                fixture.Inject(mock);
            }
        }

        private class HttpRequestBaseMockCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var mock = new Mock<HttpRequestBase>(MockBehavior.Strict);
                mock.Setup(r => r.UserHostAddress).Returns("127.0.0.1");

                fixture.Inject(mock);
            }
        }

        private class HttpResponseBaseMockCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var mock = new Mock<HttpResponseBase>(MockBehavior.Strict);

                fixture.Inject(mock);
            }
        }

        private class PrincipalMockCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var identityMock = fixture.CreateAnonymous<Mock<IIdentity>>();
                var mock = new Mock<IPrincipal>();
                mock.Setup(x => x.Identity).Returns(identityMock.Object);

                fixture.Inject(mock);
            }
        }

        private class IdentityMockCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var mock = new Mock<IIdentity>();

                fixture.Inject(mock);
            }
        }
    }
}
