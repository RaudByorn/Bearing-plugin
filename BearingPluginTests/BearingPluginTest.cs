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
        [TestCase(4, 5, 3, 1, 1, TestName = "positive")]

        public void PositiveParams(double bearingWidth, double innerRimRad, double innerRimWidth, double gutterDepth, double ballRad)
        {
            
            var unused = new BearingParametrs(bearingWidth, innerRimRad, innerRimWidth, gutterDepth, ballRad);
        }

        [Test]
        [TestCase(-5, 5, 3, 1, 1, TestName = "negative")]

        public void NegativeParams(double bearingWidth, double innerRimRad, double innerRimWidth, double gutterDepth, double ballRad)
        {
            NUnit.Framework.Assert.That(() =>
            {
                new BearingParametrs(bearingWidth, innerRimRad, innerRimWidth, gutterDepth, ballRad);
            }, Throws.TypeOf(typeof(ArgumentException)));
        }
    }
}
