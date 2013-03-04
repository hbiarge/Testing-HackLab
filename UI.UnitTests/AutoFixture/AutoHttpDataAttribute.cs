// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoHttpDataAttribute.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the AutoHttpDataAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.AutoFixture
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;

    public class AutoHttpDataAttribute : AutoDataAttribute
    {
        public AutoHttpDataAttribute()
            : base(new Fixture()
                .Customize(new HttpMocksCustomization()))
        {
        }
    }
}
