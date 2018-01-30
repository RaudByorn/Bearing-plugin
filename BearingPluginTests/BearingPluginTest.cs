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
        [TestCase(1, 3, 3, 8, 0.7, 1.6,  TestName = "[+] TestBearingRightProp 1")]
        [TestCase(1, 7, 15, 28, 2, 3.18, TestName = "[+] TestBearingRightProp 2")]
        [TestCase(1, 16, 75, 105, 4, 8.73, TestName = "[+] TestBearingRightProp 3")]

        public void TestBearingParamsRightProp(RollingElementForm rollingElementForm, double bearingWidth, 
            double innerRimDiam, double outerRimDiam, double rimsThickness, double rollingElementDiam)
        {
            var unused = new BearingParametrs(rollingElementForm, bearingWidth, innerRimDiam, outerRimDiam, rimsThickness, rollingElementDiam);
        }

        [Test]
        [TestCase(1, -3, 3, 8, 0.7, 1.6, TestName = "[-] TestBearingNegativeParams 1")]
        [TestCase(1, 7, -15, 28, 2, 3.18, TestName = "[-] TestBearingNegativeParams 2")]
        [TestCase(1, 16, 75, -105, 4, 8.73, TestName = "[-] TestBearingNegativeParams 3")]
        [TestCase(1, 3, 3, 8, -0.7, 1.6, TestName = "[-] TestBearingNegativeParams 4")]
        [TestCase(1, 7, 15, 28, 2, -3.18, TestName = "[-] TestBearingNegativeParams 5")]
        [TestCase(1, 3, 8, 3, 0.7, 1.6, TestName = "[-] TestBearingRightProp innerRimDiam > outerRimDiam")]
        [TestCase(1, 7, 15, 15, 2, 3.18, TestName = "[-] TestBearingRightProp innerRimDiam = outerRimDiam")]
        [TestCase(1, 16, 75, 105, 8, 8.73, TestName = "[-] TestBearingRightProp (outerRimDiam - innerRimDiam)/4 < rimsThickness")]
        [TestCase(1, 16, 75, 105, 0.5, 8.73, TestName = "[-] TestBearingRightProp (outerRimDiam - innerRimDiam)/4 - ballDiam / 2 > rimsThickness")]
        [TestCase(1, 7, 15, 28, 2, 8, TestName = "[-] TestBearingRightProp ballDiam > bearingWidth")]
        [TestCase(1, 3, 3, 7, 0.7, 1.6, TestName = "[-] TestBearingRightProp outerRimDiam - innerRImDiam < 5")]
        [TestCase(1, 3, 3, 8, 2.5, 1.6, TestName = "[-] TestBearingRightProp ballDiam > (outerRimDiam - innerRimDiam) / 2 - 0.2")]
        [TestCase(null, null, null, null, null, null, TestName = "[-] Enter null parametrs")]
        [TestCase(1, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, TestName = "[-] TestBearingNegativeParams double is NaN")]
        [TestCase(1, double.PositiveInfinity, double.PositiveInfinity, 
            double.PositiveInfinity, double.PositiveInfinity, 
            double.PositiveInfinity, TestName = "[-] TestBearingNegativeParams double is Positive Infinity")]
        [TestCase(1, double.NegativeInfinity, double.NegativeInfinity, 
            double.NegativeInfinity, double.NegativeInfinity, 
            double.NegativeInfinity, TestName = "[-] TestBearingNegativeParams double is Negative Infinity")]


        public void TestBearingNegativeParams(RollingElementForm rollingElementForm, double bearingWidth, 
            double innerRimDiam, double outerRimDiam, double rimsThickness, double rollingElementDiam)
        {
            NUnit.Framework.Assert.That(() =>
            {
                new BearingParametrs(rollingElementForm, bearingWidth, innerRimDiam, outerRimDiam, rimsThickness, rollingElementDiam);
            }, Throws.TypeOf(typeof(ArgumentException)));
        }
    }
}
