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

            Assert.IsNotNull(modelView.Balls, "Lista kulek powinna byæ zainicjalizowana.");
            Assert.AreEqual(0, modelView.Balls.Count, "Lista kulek powinna byæ pusta po utworzeniu.");
            Assert.AreEqual(700, modelView._width, "Domyœlna szerokoœæ powinna wynosiæ 700.");
            Assert.AreEqual(500, modelView._height, "Domyœlna wysokoœæ powinna wynosiæ 500.");
        }

        [TestMethod]
        public void SummonBalls_WithCorrectAmount_ShouldAddBalls()
        {
            var modelView = new ModelView();
            modelView.Amount = "7";

            modelView.SummonCommand.Execute(null);

            Assert.AreEqual(7, modelView.Balls.Count, "Liczba kulek po przywo³aniu powinna wynosiæ 7.");
        }

        [TestMethod]
        public void SummonBalls_WithInvalidAmount_ShouldResetAmount()
        {
            var modelView = new ModelView();
            modelView.Amount = "-3";

            modelView.SummonCommand.Execute(null);

            Assert.AreEqual("", modelView.Amount, "Wartoœæ Amount powinna byæ zresetowana po b³êdnym wejœciu.");
            Assert.AreEqual(0, modelView.Balls.Count, "Nie powinno byæ ¿adnych kulek po b³êdnym przywo³aniu.");
        }

        [TestMethod]
        public void ClearBalls_ShouldRemoveAllBallsAndResetAmount()
        {
            var modelView = new ModelView();
            modelView.Amount = "4";

            modelView.SummonCommand.Execute(null);
            Assert.AreEqual(4, modelView.Balls.Count, "Powinno byæ 4 kulki przed czyszczeniem.");

            modelView.ClearCommand.Execute(null);

            Assert.AreEqual(0, modelView.Balls.Count, "Lista kulek powinna byæ pusta po wyczyszczeniu.");
            Assert.AreEqual("", modelView.Amount, "Wartoœæ Amount powinna byæ wyczyszczona.");
        }
    }
}
