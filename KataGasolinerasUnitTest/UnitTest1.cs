using System;
using System.Drawing;
using KataGasolineras.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subgurim.Controles;
using KataGasolineras.Model.Extensions;

namespace KataGasolinerasUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetRandomLat()
        {
            for (var i = 0; i < 91; i++)
            {
                for (var c = -180; c < 181; c++)
                {
                    try
                    {
                        new GLatLng(i, c).GetRandomLat(5000, 10000);
                    }
                    catch
                    {
                        Assert.Fail();
                    }

                }
            }
        }

        [TestMethod]
        public void TestGetRandomLon()
        {
            for (var i = 0; i < 91; i++)
            {
                for (var c = -180; c < 181; c++)
                {

                    try
                    {
                        new GLatLng(i, c).GetRandomLon(5000, 10000);
                    }
                    catch
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        [TestMethod]
        public void CalculateInitialRemainigDistance()
        {
            Assert.AreEqual(Route.CalculateInitialRemainigDistance(1, 1), 100000);
        }
    }
}
