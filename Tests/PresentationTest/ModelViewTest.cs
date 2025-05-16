using Presentation.ModelView;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PresentationTest
{
    [TestClass]
    public class ModelViewTest
    {
        private ModelView vm;

        [TestInitialize]
        public void Setup()
        {
            vm = new ModelView();
        }

        [TestMethod]
        public void InitialState_ShouldReflectDefaults()
        {
            Assert.AreEqual(1000, vm.CanvasWidth);
            Assert.AreEqual(600, vm.CanvasHeight);
            Assert.AreEqual("", vm.BallCount);
            Assert.IsTrue(vm.AddCommand.CanExecute(null));
            Assert.IsFalse(vm.ClearCommand.CanExecute(null));
            Assert.IsTrue(vm.StartCommand.CanExecute(null));
            Assert.IsFalse(vm.StopCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddBalls_ValidCount_PopulatesBallsAndEnablesClear()
        {
            vm.BallCount = "3";
            vm.AddCommand.Execute(null);
            Assert.AreEqual(3, vm.Balls.Count);
            Assert.IsTrue(vm.ClearCommand.CanExecute(null));
            Assert.IsFalse(vm.AddCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddBalls_InvalidCount_ClearsBallCount()
        {
            vm.BallCount = "xyz";
            vm.AddCommand.Execute(null);
            Assert.AreEqual("", vm.BallCount);
            Assert.IsTrue(vm.AddCommand.CanExecute(null));
        }

        [TestMethod]
        public void StartAndStopSimulation_ToggleCommands()
        {
            vm.BallCount = "2";
            vm.AddCommand.Execute(null);
            vm.StartCommand.Execute(null);
            Assert.IsFalse(vm.StartCommand.CanExecute(null));
            Assert.IsTrue(vm.StopCommand.CanExecute(null));

            vm.StopCommand.Execute(null);
            Assert.IsTrue(vm.StartCommand.CanExecute(null));
            Assert.IsFalse(vm.StopCommand.CanExecute(null));
        }

        [TestMethod]
        public void ClearBalls_ResetsState()
        {
            vm.BallCount = "2";
            vm.AddCommand.Execute(null);
            vm.ClearCommand.Execute(null);
            Assert.AreEqual(0, vm.Balls.Count);
            Assert.AreEqual("", vm.BallCount);
            Assert.IsFalse(vm.ClearCommand.CanExecute(null));
            Assert.IsTrue(vm.AddCommand.CanExecute(null));
        }

        [TestMethod]
        public void PropertyChanged_RaisesForCanvasWidthAndBallCount()
        {
            bool widthChanged = false;
            bool countChanged = false;
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(vm.CanvasWidth)) widthChanged = true;
                if (e.PropertyName == nameof(vm.BallCount)) countChanged = true;
            };

            vm.CanvasWidth = 800;
            vm.BallCount = "5";

            Assert.IsTrue(widthChanged);
            Assert.IsTrue(countChanged);
        }
    }
}
