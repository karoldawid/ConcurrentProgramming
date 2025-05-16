using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.Model;
using Presentation.ModelView;
using System.Linq;
using System.ComponentModel;
using Data;
using Logic;
using System;

namespace PresentationTests
{
    [TestClass]
    public class ModelTests
    {
        private MainAPI model;

        [TestInitialize]
        public void Setup()
        {
            model = MainAPI.GenerateTable(400, 300);
        }

        [TestMethod]
        public void TestGenerateTable_CreatesEmptyBalls()
        {
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.GetBalls().Count);
        }

        [TestMethod]
        public void TestGenerateBalls_AddsSpecifiedNumber()
        {
            model.GenerateBalls(4);
            var list = model.GetBalls();
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(4, model.Balls.Count);
        }

        [TestMethod]
        public void TestClearMap_EmptiesBalls()
        {
            model.GenerateBalls(2);
            Assert.IsTrue(model.GetBalls().Count > 0);
            model.ClearMap();
            Assert.AreEqual(0, model.GetBalls().Count);
            Assert.AreEqual(0, model.Balls.Count);
        }

        [TestMethod]
        public void TestPauseMovement_DoesNotThrow()
        {
            try
            {
                model.GenerateBalls(1);
                model.PauseMovement();
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail($"PauseMovement threw: {ex.Message}");
            }
        }
    }
}