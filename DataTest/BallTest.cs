using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void Ball_ShouldBeInitializedWithCorrectProperties()
        {
            var ball = BallAPI.Generate(1, 0, 1, 2, "blue", 1, 2);

            Assert.AreEqual(1, ball.ID, "ID kulki nie zosta³ poprawnie ustawiony.");
            Assert.AreEqual(0, ball.X, "Pozycja X kulki nie zosta³a poprawnie ustawiona.");
            Assert.AreEqual(1, ball.Y, "Pozycja Y kulki nie zosta³a poprawnie ustawiona.");
            Assert.AreEqual(2, ball.Radius, "Promieñ kulki jest niepoprawny.");
            Assert.AreEqual(1, ball.XDirection, "Kierunek X kulki jest niepoprawny.");
            Assert.AreEqual(2, ball.YDirection, "Kierunek Y kulki jest niepoprawny.");
            Assert.AreEqual("blue", ball.Color, "Kolor kulki nie zosta³ poprawnie ustawiony.");
        }
    }
}
