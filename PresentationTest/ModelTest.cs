using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.Model;
using System.Linq;

namespace PresentationTest
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void CreatingBallsAndShouldAddExpectedNumberOfBalls()
        {
            var model = MainAPI.CreateMap(500, 500);

            model.CreateBalls(6);
            var balls = model.GetBalls();

            Assert.AreEqual(6, balls.Count, "Liczba kulek w modelu nie jest zgodna z oczekiwan¹.");
        }

        [TestMethod]
        public void ClearingMapAndShouldRemoveAllBallsFromModel()
        {
            var model = MainAPI.CreateMap(500, 500);

            model.CreateBalls(4);
            model.ClearMap();

            Assert.AreEqual(0, model.GetBalls().Count, "Mapa powinna byæ pusta po wyczyszczeniu.");
        }

        [TestMethod]
        public void MoveAndShouldChangeBallPosition()
        {
            var model = MainAPI.CreateMap(500, 500);

            model.CreateBalls(1);
            var ballBeforeMove = model.GetBalls().First();
            double startX = ballBeforeMove.X;
            double startY = ballBeforeMove.Y;

            model.Move();
            var ballAfterMove = model.GetBalls().First();

            bool hasPositionChanged = startX != ballAfterMove.X || startY != ballAfterMove.Y;
            Assert.IsTrue(hasPositionChanged, "Po wykonaniu ruchu kulka powinna zmieniæ pozycjê.");
        }
    }
}
