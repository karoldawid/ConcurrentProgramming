using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace LogicTests
{
    [TestClass]
    public class BallLogicTests
    {
        private BallLogicAPI logic;

        [TestInitialize]
        public void Setup()
        {
            logic = BallLogicAPI.GenerateLogic(500, 400);
        }

        [TestMethod]
        public void TestGenerateBall_IncreasesCount()
        {
            int initial = logic.GetBalls().Count;
            logic.GenerateBall(1, 50, 50, 5, "red", 1, 1);
            int after = logic.GetBalls().Count;
            Assert.AreEqual(initial + 1, after);
        }

        [TestMethod]
        public void TestGenerateBallSet_CreatesCorrectNumber()
        {
            logic.GenerateBallSet(3);
            Assert.AreEqual(3, logic.GetBalls().Count);
        }

        [TestMethod]
        public void TestGenerateRandomBall_AddsBall()
        {
            int initial = logic.GetBalls().Count;
            logic.GenerateRandomBall();
            Assert.AreEqual(initial + 1, logic.GetBalls().Count);
        }

        [TestMethod]
        public void TestClearTable_RemovesAllBalls()
        {
            logic.GenerateRandomBall();
            logic.GenerateRandomBall();
            Assert.IsTrue(logic.GetBalls().Count > 0);
            logic.ClearTable();
            Assert.AreEqual(0, logic.GetBalls().Count);
        }

        [TestMethod]
        public void TestGetTableDimensions_ReturnsConstructorValues()
        {
            Assert.AreEqual(500, logic.GetTableWidth());
            Assert.AreEqual(400, logic.GetTableHeight());
        }

        [TestMethod]
        public void TestDistance_CalculatesCorrectly()
        {
            var a = BallDataAPI.GenerateBall(1, 0, 0, 3, "blue", 0, 0, 1);
            var b = BallDataAPI.GenerateBall(2, 3, 4, 3, "green", 0, 0, 1);
            double d = logic.Distance(a, b);
            Assert.AreEqual(5.0, d, 1e-6);
        }

        [TestMethod]
        public void TestCollisionEvent_DetectsCollision()
        {
            var a = BallDataAPI.GenerateBall(1, 0, 0, 5, "blue", 0, 0, 1);
            var b = BallDataAPI.GenerateBall(2, 8, 0, 5, "green", 0, 0, 1);
            Assert.IsTrue(logic.CollisionEvent(a, b));
            var c = BallDataAPI.GenerateBall(3, 20, 20, 5, "yellow", 0, 0, 1);
            Assert.IsFalse(logic.CollisionEvent(a, c));
        }

        [TestMethod]
        public void TestCollidingBalls_ReturnsCorrectBall()
        {
            var a = BallDataAPI.GenerateBall(1, 10, 10, 5, "blue", 0, 0, 1);
            var b = BallDataAPI.GenerateBall(2, 14, 10, 5, "green", 0, 0, 1);

            logic.ClearTable();
            var dm = logic.GetType()
                .GetField("dataManager", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(logic);
            var addMethod = dm.GetType().GetMethod("AddBall");
            addMethod.Invoke(dm, new object[] { a });
            addMethod.Invoke(dm, new object[] { b });

            var hit = logic.CollidingBalls(a);
            Assert.IsNotNull(hit);
            Assert.AreEqual(b.ID, hit.ID);
        }

        [TestMethod]
        public void TestRunPauseAnimation_DoesNotThrow()
        {
            logic.GenerateBall(1, 100, 100, 5, "red", 1, 1);
            try
            {
                logic.RunAnimation();
                Thread.Sleep(100);
                logic.PauseAnimation();
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Animation methods threw: {ex.Message}");
            }
        }
    }
}
