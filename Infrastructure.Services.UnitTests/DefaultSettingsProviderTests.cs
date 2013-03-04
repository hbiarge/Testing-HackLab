// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultSettingsProviderTests.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the DefaultSettingsProviderTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.Infrastructure.Services.UnitTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem("App.config")]
    public class DefaultSettingsProviderTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetValueWithNullKeyThrows()
        {
            var sut = new DefaultSettingsProvider();

            sut.GetValue<string>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetValueWithNotExistingKeyThrows()
        {
            var sut = new DefaultSettingsProvider();

            sut.GetValue<string>("MustThrow");
        }

        [TestMethod]
        public void GetValueWithExistingKeyAsString()
        {
            var sut = new DefaultSettingsProvider();

            var value = sut.GetValue<string>("test.string");

            Assert.AreEqual("hola", value);
        }

        [TestMethod]
        public void GetValueWithExistingKeyAsInteger()
        {
            var sut = new DefaultSettingsProvider();

            var value = sut.GetValue<int>("test.int");

            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void GetValueWithExistingKeyAsDouble()
        {
            var sut = new DefaultSettingsProvider();

            var value = sut.GetValue<double>("test.double");

            Assert.AreEqual(1.1, value);
        }

        [TestMethod]
        public void GetValueWithExistingKeyAsDateYYYYMMDD()
        {
            var sut = new DefaultSettingsProvider();

            var value = sut.GetValue<DateTime>("test.date.YYYYMMDD");

            Assert.AreEqual(2013, value.Year);
            Assert.AreEqual(2, value.Month);
            Assert.AreEqual(28, value.Day);
        }

        [TestMethod]
        public void GetValueWithExistingKeyAsDateMMDDYYYY()
        {
            var sut = new DefaultSettingsProvider();

            var value = sut.GetValue<DateTime>("test.date.MMDDYYYY");

            Assert.AreEqual(2013, value.Year);
            Assert.AreEqual(2, value.Month);
            Assert.AreEqual(28, value.Day);
        }
    }
}
