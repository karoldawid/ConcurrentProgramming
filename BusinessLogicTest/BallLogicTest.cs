using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogicTest
{
    [TestClass]
    public class BallLogicTest
    {
        [TestMethod]
        public void BallMove()
        {
            var ballLogic = BallLogicAPI.Initialize(150, 250);
            Assert.AreEqual(150, ballLogic.GetMapWidth(), "Niepoprawna szerokość mapy.");
            Assert.AreEqual(250, ballLogic.GetMapHeight(), "Niepoprawna wysokość mapy.");

            ballLogic.CreateNBalls(7);
            double initialX = ballLogic.GetBallByID(2).X;
            double initialY = ballLogic.GetBallByID(2).Y;

            ballLogic.UpdateBalls();

            double updatedX = ballLogic.GetBallByID(2).X;
            double updatedY = ballLogic.GetBallByID(2).Y;

            Assert.AreNotEqual(initialX, updatedX, "Pozycja X nie zmieniła się po aktualizacji.");
            Assert.AreNotEqual(initialY, updatedY, "Pozycja Y nie zmieniła się po aktualizacji.");
        }

        [TestMethod]
        public void BallsWithinMapBounds()
        {
            var ballLogic = BallLogicAPI.Initialize(200, 300);
            ballLogic.CreateNBalls(12);

            foreach (var ball in ballLogic.GetBalls())
            {
                Assert.IsTrue(ball.X - ball.Radius >= 0, "Kulka wychodzi poza lewą granicę mapy.");
                Assert.IsTrue(ball.X + ball.Radius <= ballLogic.GetMapWidth(), "Kulka wychodzi poza prawą granicę mapy.");
                Assert.IsTrue(ball.Y - ball.Radius >= 0, "Kulka wychodzi poza górną granicę mapy.");
                Assert.IsTrue(ball.Y + ball.Radius <= ballLogic.GetMapHeight(), "Kulka wychodzi poza dolną granicę mapy.");
            }
        }

        [TestMethod]
        public void ClearMap()
        {
            var ballLogic = BallLogicAPI.Initialize(120, 220);
            ballLogic.CreateNBalls(8);

            Assert.AreEqual(8, ballLogic.GetSize(), "Niepoprawna liczba kulek po utworzeniu.");

            ballLogic.ClearMap();

            Assert.AreEqual(0, ballLogic.GetSize(), "Magazyn kulek powinien być pusty po wyczyszczeniu mapy.");
        }

        [TestMethod]
        public void ConstructorShouldInitializeAndHandleBallOperations()
        {
            var ballLogic = BallLogicAPI.Initialize(130, 270);
            Assert.AreEqual(130, ballLogic.GetMapWidth(), "Zła szerokość mapy po inicjalizacji.");
            Assert.AreEqual(270, ballLogic.GetMapHeight(), "Zła wysokość mapy po inicjalizacji.");

            ballLogic.CreateNBalls(6);
            Assert.AreEqual(6, ballLogic.GetSize(), "Niepoprawna liczba kulek po utworzeniu.");

            Assert.AreEqual(3, ballLogic.GetBallByID(3).ID, "Nieprawidłowe ID kulki.");

            ballLogic.DeleteBall(ballLogic.GetBallByID(3));
            Assert.AreEqual(5, ballLogic.GetSize(), "Niepoprawna liczba kulek po usunięciu jednej.");

            ballLogic.ClearMap();
            Assert.AreEqual(0, ballLogic.GetSize(), "Wszystkie kulki powinny być usunięte po wyczyszczeniu mapy.");
        }
    }
}
