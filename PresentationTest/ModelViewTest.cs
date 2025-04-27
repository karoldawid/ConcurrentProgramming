using Presentation.ModelView;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PresentationTest
{
    [TestClass]
    public class ModelViewTest
    {
        [TestMethod]
        public void ModelView_ShouldInitializeWithDefaultValues()
        {
            var modelView = new ModelView();

            Assert.IsNotNull(modelView.Balls, "Lista kulek powinna by� zainicjalizowana.");
            Assert.AreEqual(0, modelView.Balls.Count, "Lista kulek powinna by� pusta po utworzeniu.");
            Assert.AreEqual(700, modelView._width, "Domy�lna szeroko�� powinna wynosi� 700.");
            Assert.AreEqual(500, modelView._height, "Domy�lna wysoko�� powinna wynosi� 500.");
        }

        [TestMethod]
        public void SummonBalls_WithCorrectAmount_ShouldAddBalls()
        {
            var modelView = new ModelView();
            modelView.Amount = "7";

            modelView.SummonCommand.Execute(null);

            Assert.AreEqual(7, modelView.Balls.Count, "Liczba kulek po przywo�aniu powinna wynosi� 7.");
        }

        [TestMethod]
        public void SummonBalls_WithInvalidAmount_ShouldResetAmount()
        {
            var modelView = new ModelView();
            modelView.Amount = "-3";

            modelView.SummonCommand.Execute(null);

            Assert.AreEqual("", modelView.Amount, "Warto�� Amount powinna by� zresetowana po b��dnym wej�ciu.");
            Assert.AreEqual(0, modelView.Balls.Count, "Nie powinno by� �adnych kulek po b��dnym przywo�aniu.");
        }

        [TestMethod]
        public void ClearBalls_ShouldRemoveAllBallsAndResetAmount()
        {
            var modelView = new ModelView();
            modelView.Amount = "4";

            modelView.SummonCommand.Execute(null);
            Assert.AreEqual(4, modelView.Balls.Count, "Powinno by� 4 kulki przed czyszczeniem.");

            modelView.ClearCommand.Execute(null);

            Assert.AreEqual(0, modelView.Balls.Count, "Lista kulek powinna by� pusta po wyczyszczeniu.");
            Assert.AreEqual("", modelView.Amount, "Warto�� Amount powinna by� wyczyszczona.");
        }
    }
}
