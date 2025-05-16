using System;
using System.ComponentModel;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Linq;

namespace DataTests
{
    [TestClass]
    public class BallDataAPITests
    {
        private BallDataAPI ball;

        [TestInitialize]
        public void Setup()
        {
            ball = BallDataAPI.GenerateBall(1, 50, 50, 10, "red", 1, 1, 2);
        }

        [TestMethod]
        public void TestInitialization_SetsProperties()
        {
            Assert.AreEqual(1, ball.ID);
            Assert.AreEqual(50, ball.X);
            Assert.AreEqual(50, ball.Y);
            Assert.AreEqual(10, ball.r);
            Assert.AreEqual("red", ball.colour);
            Assert.AreEqual(1, ball.dirX);
            Assert.AreEqual(1, ball.dirY);
            Assert.AreEqual(2, ball.mass);
            Assert.AreEqual(1, ball.velX);
            Assert.AreEqual(1, ball.velY);
            Assert.AreEqual(50, ball.posX);
            Assert.AreEqual(50, ball.posY);
            Assert.IsTrue(ball.IsActive);
        }

        [TestMethod]
        public void TestPropertyChanged_EventFires_OnVelXChange()
        {
            bool changed = false;
            ball.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(ball.velX)) changed = true; };
            ball.velX = 5;
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void TestPropertyChanged_EventFires_OnPosYChange()
        {
            bool changed = false;
            ball.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(ball.posY)) changed = true; };
            ball.posY = 75;
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void TestUpdateBall_ReversesDirection_AtEdges()
        {
            ball.posX = 95;
            ball.posY = 95;
            ball.velX = 10;
            ball.velY = 5;
            ball.dirX = 1;
            ball.dirY = -1;
            ball.UpdateBall(100, 100);
            Assert.AreEqual(-1, ball.dirX);
            Assert.AreEqual(-1, ball.dirY);
        }

        [TestMethod]
        public void TestUpdateBall_MovesPosition_WhenWithinBounds()
        {
            ball.posX = 20;
            ball.posY = 30;
            ball.velX = 2;
            ball.velY = 3;
            ball.dirX = 1;
            ball.dirY = 1;
            ball.UpdateBall(100, 100);
            Assert.AreEqual(22, ball.posX);
            Assert.AreEqual(33, ball.posY);
        }
    }
}