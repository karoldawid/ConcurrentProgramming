using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTest
{
    [TestClass]
    public class BallRepositoryTest
    {
        [TestMethod]
        public void RepositoryShouldHandleBasicOperationsCorrectly()
        {
            var firstBall = BallAPI.Generate(1, 0, 1, 2, "green", -1, 2);
            var secondBall = BallAPI.Generate(1, 0, 1, 2, "purple", -1, 2);
            var repository = BallRepositoryAPI.CreateRepo();

            Assert.AreEqual(0, repository.GetSize(), "Repozytorium nie powinno zawiera� �adnych kulek na pocz�tku.");

            repository.AddBall(firstBall);
            Assert.AreEqual(1, repository.GetSize(), "Dodanie pierwszej kulki nie zwi�kszy�o poprawnie rozmiaru repozytorium.");

            repository.AddBall(secondBall);
            Assert.AreEqual(2, repository.GetSize(), "Dodanie drugiej kulki nie zwi�kszy�o poprawnie rozmiaru repozytorium.");

            repository.RemoveBall(secondBall);
            Assert.AreEqual(1, repository.GetSize(), "Usuni�cie kulki nie zmniejszy�o poprawnie rozmiaru repozytorium.");

            repository.ClearStorage();
            Assert.AreEqual(0, repository.GetSize(), "Repozytorium powinno by� puste po wyczyszczeniu.");
        }
    }
}
