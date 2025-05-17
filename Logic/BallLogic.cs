using System;
using System.Collections.Generic;
using System.ComponentModel;
using Data;

namespace Logic
{
    public abstract class BallLogicAPI
    {
        public static BallLogicAPI GenerateLogic(double tableWidth, double tableHeight)
        {
            return new BallLogic(tableWidth, tableHeight);
        }

        public abstract double GetTableWidth();
        public abstract double GetTableHeight();
        public abstract List<BallDataAPI> GetBalls();
        public abstract void ClearTable();
        public abstract void GenerateBall(int ID, double x, double y, double r, string colour, int dirX, int dirY);
        public abstract void GenerateBallSet(int amount);
        public abstract void GenerateRandomBall();
        public abstract void Collision();
        public abstract BallDataAPI? CollidingBalls(BallDataAPI actualBall);
        public abstract bool CollisionEvent(BallDataAPI ballA, BallDataAPI ballB);
        public abstract void ElasticRebound(BallDataAPI ballA, BallDataAPI ballB);
        public abstract double Distance(BallDataAPI ballA, BallDataAPI ballB);
        public abstract void WallCollision(BallDataAPI ball);
        public abstract void RunAnimation();
        public abstract void PauseAnimation();
        public abstract void UpdateBall(BallDataAPI ball);

        private class BallLogic : BallLogicAPI
        {
            private readonly double tableWidth;
            private readonly double tableHeight;
            private readonly DataManagerAPI dataManager;
            private bool _duringAction;

            public BallLogic(double tableWidth, double tableHeight)
            {
                this.tableWidth = tableWidth;
                this.tableHeight = tableHeight;
                this.dataManager = DataManagerAPI.GenerateData(tableWidth, tableHeight, "white");
                _duringAction = false;
            }

            public override double GetTableWidth() => tableWidth;
            public override double GetTableHeight() => tableHeight;

            public override List<BallDataAPI> GetBalls() => dataManager.GetBalls();
            public override void ClearTable()
            {
                PauseAnimation();
                dataManager.ClearBalls();
            }

            public override void GenerateBall(int ID, double x, double y, double r, string colour, int dirX, int dirY)
            {
                Random random = new Random();
                double mass = (double)random.Next(1, 10);
                r = mass + 20;

                int maxAttempts = 50;
                bool isPlaced = false;

                for (int i = 0; i < maxAttempts && !isPlaced; i++)
                {
                    x = random.Next((int)r, (int)(tableWidth - r));
                    y = random.Next((int)r, (int)(tableHeight - r));

                    BallDataAPI ball = BallDataAPI.GenerateBall(ID, x, y, r, colour, dirX, dirY, mass);
                    isPlaced = true;

                    foreach (var other in GetBalls())
                    {
                        if (CollisionEvent(ball, other))
                        {
                            isPlaced = false;
                            break;
                        }
                    }

                    if (isPlaced) dataManager.AddBall(ball);
                }
            }

            public override void GenerateBallSet(int amount)
            {
                for (int i = 0; i < amount; i++) GenerateRandomBall();
            }

            public override void GenerateRandomBall()
            {
                double r = 0;
                Random random = new Random();
                int dirX = random.Next(2) == 0 ? -1 : 1;
                int dirY = random.Next(2) == 0 ? -1 : 1;
                double x = random.Next((int)r, (int)tableWidth - (int)r);
                double y = random.Next((int)r, (int)tableHeight - (int)r);
                string colour = "#" + random.Next(0x1000000).ToString("X6");

                GenerateBall(dataManager.GetBalls().Count + 1, x, y, r, colour, dirX, dirY);
            }

            public override void Collision()
            {
                foreach (var ball in GetBalls())
                {
                    var other = CollidingBalls(ball);
                    if (other != null) ElasticRebound(ball, other);
                }
            }

            public override BallDataAPI? CollidingBalls(BallDataAPI actualBall)
            {
                foreach (var ball in GetBalls())
                {
                    if (!ball.Equals(actualBall) && CollisionEvent(actualBall, ball))
                        return ball;
                }
                return null;
            }

            public override bool CollisionEvent(BallDataAPI ballA, BallDataAPI ballB)
            {
                return Distance(ballA, ballB) <= (ballA.r + ballB.r);
            }

            public override double Distance(BallDataAPI ballA, BallDataAPI ballB)
            {
                return Math.Sqrt(Math.Pow(ballA.X - ballB.X, 2) + Math.Pow(ballA.Y - ballB.Y, 2));
            }

            public override void ElasticRebound(BallDataAPI ballA, BallDataAPI ballB)
            {
                int tempX = ballA.dirX;
                int tempY = ballA.dirY;
                ballA.dirX = ballB.dirX;
                ballA.dirY = ballB.dirY;
                ballB.dirX = tempX;
                ballB.dirY = tempY;
            }

            public override void WallCollision(BallDataAPI ball)
            {
                if (ball.X - ball.r <= 0 || ball.X + ball.r >= tableWidth)
                {
                    ball.dirX = -ball.dirX;
                    ball.X = Math.Clamp(ball.X, ball.r, tableWidth - ball.r);
                }

                if (ball.Y - ball.r <= 0 || ball.Y + ball.r >= tableHeight)
                {
                    ball.dirY = -ball.dirY;
                    ball.Y = Math.Clamp(ball.Y, ball.r, tableHeight - ball.r);
                }
            }

            public override void RunAnimation()
            {
                _duringAction = true;
                Task.Run(() =>
                {
                    while (_duringAction)
                    {
                        foreach (var ball in GetBalls())
                        {
                            ball.IsActive = true;
                            UpdateBall(ball);
                        }
                        Thread.Sleep(16);
                    }
                });
            }

            public override void PauseAnimation()
            {
                _duringAction = false;
            }

            public override void UpdateBall(BallDataAPI ball)
            {
                if (!ball.IsActive) return;

                WallCollision(ball);
                var otherBall = CollidingBalls(ball);
                if (otherBall != null) ElasticRebound(ball, otherBall);

                ball.X += ball.dirX;
                ball.Y += ball.dirY;
            }

            private void isMoving(object sender, PropertyChangedEventArgs e)
            {
                BallDataAPI ball = (BallDataAPI)sender;
                if (e.PropertyName == nameof(BallDataAPI.posX) || e.PropertyName == nameof(BallDataAPI.posY))
                {
                    UpdateBall(ball);
                    ball.IsActive = false;
                }
            }
        }
    }
}