using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultidiskBackup;


namespace MultidiskBackupTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BasicTestOfEstimatedTimeCalculation()
        {

            Assert.AreEqual(TimeSpan.FromMinutes(3),HelperFunctions.GetEstimatedTime(TimeSpan.FromMinutes(3).Ticks, 500, 1000));
        }
        [TestMethod]
        public void MoreAdvancedTestOfEstimatedTimeCalculation()
        {

            Assert.AreEqual(TimeSpan.FromMinutes(1), HelperFunctions.GetEstimatedTime(TimeSpan.FromMinutes(3).Ticks, 750, 1000));
        }
        [TestMethod]
        public void RealisticTestOfEstimatedTimeCalculation()
        {

            Assert.AreEqual(TimeSpan.FromMinutes(5), HelperFunctions.GetEstimatedTime(TimeSpan.FromMinutes(25).Ticks, 2543231214325, 3051877457190));
        }
    }
}
