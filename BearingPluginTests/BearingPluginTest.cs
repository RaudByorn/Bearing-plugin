﻿using System;
using BearingPlugin;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BearingPluginTests
{
    [TestFixture]
    public class BearingPluginTest
    {
        [Test]
        [TestCase(3, 3, 8, 0.7, 1.6,  TestName = "[+] TestBearingRightProp 1")]
        [TestCase(7, 15, 28, 2, 3.18, TestName = "[+] TestBearingRightProp 2")]
        [TestCase(16, 75, 105, 4, 8.73, TestName = "[+] TestBearingRightProp 3")]

        public void TestBearingParamsRightProp(double bearingWidth, double innerRimDiam, double outerRimDiam, double rimsThickness, double ballDiam)
        {
            var unused = new BearingParametrs(bearingWidth, innerRimDiam, outerRimDiam, rimsThickness, ballDiam);
        }

        [Test]
        [TestCase(-3, 3, 8, 0.7, 1.6, TestName = "[-] TestBearingNegativeParams 1")]
        [TestCase(7, -15, 28, 2, 3.18, TestName = "[-] TestBearingNegativeParams 2")]
        [TestCase(16, 75, -105, 4, 8.73, TestName = "[-] TestBearingNegativeParams 3")]
        [TestCase(3, 3, 8, -0.7, 1.6, TestName = "[-] TestBearingNegativeParams 4")]
        [TestCase(7, 15, 28, 2, -3.18, TestName = "[-] TestBearingNegativeParams 5")]

        public void TestBearingNegativeParams(double bearingWidth, double innerRimDiam, double outerRimDiam, double rimsThickness, double ballDiam)
        {
            NUnit.Framework.Assert.That(() =>
            {
                new BearingParametrs(bearingWidth, innerRimDiam, outerRimDiam, rimsThickness, ballDiam);
            }, Throws.TypeOf(typeof(ArgumentException)));
        }

        [Test]
        [TestCase(3, 8, 3, 0.7, 1.6, TestName = "[-] TestBearingRightProp innerRimDiam > outerRimDiam")]
        [TestCase(7, 15, 15, 2, 3.18, TestName = "[-] TestBearingRightProp innerRimDiam = outerRimDiam")]
        [TestCase(16, 75, 105, 8, 8.73, TestName = "[-] TestBearingRightProp (outerRimDiam - innerRimDiam)/4 < rimsThickness")]
        [TestCase(16, 75, 105, 0.5, 8.73, TestName = "[-] TestBearingRightProp (outerRimDiam - innerRimDiam)/4 - ballDiam / 2 > rimsThickness")]
        [TestCase(7, 15, 28, 2, 8, TestName = "[-] TestBearingRightProp ballDiam > bearingWidth")]
        [TestCase(3, 3, 7, 0.7, 1.6, TestName = "[+] TestBearingRightProp outerRimDiam - innerRImDiam < 5")]
        [TestCase(3, 3, 8, 2.5, 1.6, TestName = "[+] TestBearingRightProp ballDiam > (outerRimDiam - innerRimDiam) / 2 - 0.2")]

        public void TestBearingParamsWrongProp(double bearingWidth, double innerRimDiam, double outerRimDiam, double rimsThickness, double ballDiam)
        {
            NUnit.Framework.Assert.That(() =>
            {
                new BearingParametrs(bearingWidth, innerRimDiam, outerRimDiam, rimsThickness, ballDiam);
            }, Throws.TypeOf(typeof(ArgumentException)));
        }


    }
}
