using System;
using System.ComponentModel;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Linq;



namespace SimulationTable
{
    [TestClass]
    public class SimulationTableAPITests
    {
        private SimulationTableAPI table;

        [TestInitialize]
        public void Setup()
        {
            table = SimulationTableAPI.GenerateBoard(200, 300, "white");
        }

        [TestMethod]
        public void TestBoardInitialization_Properties()
        {
            Assert.AreEqual(300, table._tableWidth);
            Assert.AreEqual(200, table._tableHeight);
            Assert.AreEqual("white", table._colour);
        }

        [TestMethod]
        public void TestAddAndClearBalls()
        {
            var ball1 = BallDataAPI.GenerateBall(1, 10, 10, 5, "blue", 0, 0, 1);
            var ball2 = BallDataAPI.GenerateBall(2, 20, 20, 5, "green", 0, 0, 1);
            table.AddBall(ball1);
            table.AddBall(ball2);
            var list = table.GetBalls();
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.Any(b => b.ID == 1));
            table.ClearBalls();
            Assert.AreEqual(0, table.GetBalls().Count);
        }
    }
}