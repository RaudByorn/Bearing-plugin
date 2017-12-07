using System;
using BearingPlugin;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BearingPluginTests
{
    [TestFixture]
    public class BearingPluginTest
    {
        [Test]
        [TestCase(3, 3, 8, TestName = "[+] TestBearingRightProp 1")]
        [TestCase(7, 15, 28, TestName = "[+] TestBearingRightProp 2")]
        [TestCase(16, 75, 105, TestName = "[+] TestBearingRightProp 3")]

        public void TestBearingParamsRightProp(double bearingWidth, double innerRimDiam, double outerRimDiam)
        {
            var unused = new BearingParametrs(bearingWidth, innerRimDiam, outerRimDiam);
        }

        [Test]
        [TestCase(-3, 3, 8, TestName = "[-] TestBearingNegativeParams 1")]
        [TestCase(7, -15, 28, TestName = "[-] TestBearingNegativeParams 2")]
        [TestCase(16, 75, -105, TestName = "[-] TestBearingNegativeParams 3")]

        public void TestBearingNegativeParams(double bearingWidth, double innerRimDiam, double outerRimDiam)
        {
            NUnit.Framework.Assert.That(() =>
            {
                new BearingParametrs(bearingWidth, innerRimDiam, outerRimDiam);
            }, Throws.TypeOf(typeof(ArgumentException)));
        }

        [Test]
        [TestCase(3, 3, 3, TestName = "[-] TestBearingRightProp 1 | inner = outer")]
        [TestCase(7, 28, 15, TestName = "[-] TestBearingRightProp 2 | inner > outer")]
        [TestCase(16, 101, 105, TestName = "[-] TestBearingRightProp 3 | inner - outer < 5")]

        public void TestBearingParamsWrongProp(double bearingWidth, double innerRimDiam, double outerRimDiam)
        {
            NUnit.Framework.Assert.That(() =>
            {
                new BearingParametrs(bearingWidth, innerRimDiam, outerRimDiam);
            }, Throws.TypeOf(typeof(ArgumentException)));
        }


    }
}
