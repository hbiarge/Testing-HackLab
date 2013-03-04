// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocaleDateTimeBinderTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the LocaleDateTimeBinderTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.UnitTests.Infrastructure
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Web.Mvc;

    using Acheve.UI.Infrastructure;

    using FluentAssertions;

    using Ploeh.AutoFixture.Xunit;

    using Xunit.Extensions;

    public class LocaleDateTimeBinderTests
    {
        [Theory, AutoData]
        public void CanConvertLocaleDates(LocaleDateTimeBinder sut)
        {
            var collection = new NameValueCollection { { "foo", "20/12/2012" } };
            var dict = new NameValueCollectionValueProvider(collection, new CultureInfo("es-ES"));

            var modelBindingContext = new ModelBindingContext { ModelName = "foo", ValueProvider = dict };

            var model = sut.BindModel(null, modelBindingContext);

            model.Should().BeOfType<DateTime>();
            var modelDate = (DateTime)model;
            modelDate.Should().Be(new DateTime(2012, 12, 20));
        }
    }
}
