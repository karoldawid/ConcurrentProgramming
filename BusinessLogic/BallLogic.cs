using System;
using System.Timers;
using Data;
using System.Collections.Generic;

namespace BusinessLogic
{
    public abstract class BallLogicAPI
    {
        public static BallLogicAPI Initialize(int mapWidth, int mapHeight, BallRepositoryAPI? repo = null)
        {
            return new BallLogic(mapWidth, mapHeight, repo ?? BallRepositoryAPI.CreateRepo());
        }

        public abstract void AddNewBall(int ballID, double x, double y, double radius, string color, int XDirection, int YDirection);
        public abstract void DeleteBall(BallAPI ball);
        public abstract int GetMapWidth();
        public abstract int GetMapHeight();
        public abstract BallAPI GetBallByID(int id);
        public abstract List<BallAPI> GetBalls();  
        public abstract void MoveBall(BallAPI ball);
        public abstract void UpdateBalls();

        public abstract int GetSize();

        public abstract void CreateNBalls(int amount);

        public abstract void CreateRandomBall();

        public abstract void ClearMap();

        private class BallLogic(int mapWidth, int mapHeight, BallRepositoryAPI repository) : BallLogicAPI
        {
            private readonly int mapWidth = mapWidth;
            private readonly int mapHeight = mapHeight;
            private readonly BallRepositoryAPI repository = repository;

            public override void AddNewBall(int ballID, double x, double y, double radius, string color, int XDirection, int YDirection)

            {

                var ball = BallAPI.Generate(repository.GetSize() + 1, x, y, radius, color, XDirection, YDirection);  
                while (ball.X + radius > mapWidth || ball.X - radius < 0 || ball.Y + radius > mapHeight || ball.Y - radius < 0){
                    ball = BallAPI.Generate(repository.GetSize() + 1, x, y, radius, color, XDirection, YDirection);
                }
                repository.AddBall(ball);
            }

            public override void DeleteBall(BallAPI ball)
            {
                repository.RemoveBall(ball);
            }

            public override BallAPI GetBallByID(int id)
            {
                return repository.GetBallByID(id);
            }

            public override List<BallAPI> GetBalls()
            {
                return repository.GetAllBalls();
            }

            public override int GetMapWidth()
            {
                return mapWidth;
            }

            public override int GetMapHeight()
            {
                return mapHeight;
            }

            public override void MoveBall(BallAPI ball)
            {
                if (ball.X + ball.XDirection - ball.Radius < 0 || ball.X + ball.XDirection + ball.Radius > mapWidth)
                {
                    ball.XDirection = -ball.XDirection;
                }

                if (ball.Y + ball.YDirection - ball.Radius < 0 || ball.Y + ball.YDirection + ball.Radius > mapHeight)
                {
                    ball.YDirection = -ball.YDirection;
                }

                ball.X += ball.XDirection;
                ball.Y += ball.YDirection;

                if (ball.X - ball.Radius < 0)
                {
                    ball.X = ball.Radius;
                }
                else if (ball.X + ball.Radius > mapWidth)
                {
                    ball.X = mapWidth - ball.Radius;

                    if (ball.Y - ball.Radius < 0)
                    {
                        ball.Y = ball.Radius;
                    }
                    else if (ball.Y + ball.Radius > mapHeight)
                    {
                        ball.Y = mapHeight - ball.Radius;
                    }
                }
            }

            public override void UpdateBalls()
            {
                foreach (var ball in GetBalls())
                {
                    MoveBall(ball);
                }
            }

            public override void CreateNBalls(int amount){

                for(int i = 0; i< amount; i++){
                    CreateRandomBall();
                }

            }

            public override void CreateRandomBall()
            {
                double radius = 12;
                Random random = new();

                // Prostsze generowanie kierunku
                int xRandom = random.Next(0, 2) * 2 - 1; // -1 lub 1
                int yRandom = random.Next(0, 2) * 2 - 1; // -1 lub 1

                double x = random.Next((int)radius, mapWidth - (int)radius);
                double y = random.Next((int)radius, mapHeight - (int)radius);

                string color = "#0000FF"; // Stały niebieski kolor

                this.AddNewBall(repository.GetSize() + 1, x, y, radius, color, xRandom, yRandom);
            }

            public override int GetSize(){
                    return this.repository.GetSize();
                }

                public override void ClearMap()
                {
                this.repository.ClearStorage();
                }
                
                }
            }
    }


