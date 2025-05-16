using System;
using System.ComponentModel;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Linq;

namespace DataTests
{
    [TestClass]
    public class DataManagerAPITests
    {
        private DataManagerAPI manager;

        [TestInitialize]
        public void Setup()
        {
            manager = DataManagerAPI.GenerateData(100, 150, "white");
        }

        [TestMethod]
        public void TestAddAndGetBalls()
        {
            var ball = BallDataAPI.GenerateBall(1, 5, 5, 5, "red", 1, 1, 1);
            manager.AddBall(ball);
            var list = manager.GetBalls();
            Assert.AreEqual(1, list.Count);
            Assert.AreSame(ball, list.First());
        }

        [TestMethod]
        public void TestClearBalls_EmptiesList()
        {
            manager.AddBall(BallDataAPI.GenerateBall(2, 10, 10, 5, "blue", 0, 1, 1));
            Assert.IsTrue(manager.GetBalls().Count > 0);
            manager.ClearBalls();
            Assert.AreEqual(0, manager.GetBalls().Count);
        }

        [TestMethod]
        public void TestStartAndStopMovingBalls_DoesNotThrow()
        {
            manager.AddBall(BallDataAPI.GenerateBall(3, 15, 15, 5, "green", 1, 0, 1));
            try
            {
                manager.StartMovingBalls();
                Thread.Sleep(50);
                manager.StopMovingBalls();
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Movement threads failed: {ex.Message}");
            }
        }
    }
}