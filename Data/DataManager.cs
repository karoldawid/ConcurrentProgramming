using System;
using System.Collections.Generic;
using System.Threading;
using Data;

namespace Data
{
    public abstract class DataManagerAPI
    {
        public static DataManagerAPI GenerateData(double height, double width, string colour)
        {
            return new DataManager(height, width, colour);
        }

        public abstract void AddBall(BallDataAPI ball);
        public abstract List<BallDataAPI> GetBalls();
        public abstract void ClearBalls();
        public abstract void StartMovingBalls();
        public abstract void StopMovingBalls();

        private class DataManager : DataManagerAPI
        {
            private readonly SimulationTableAPI table;
            private readonly List<Thread> _threads;
            private readonly object _lock = new object();
            private bool _active;
            private readonly double height;
            private readonly double width;

            public DataManager(double height, double width, string colour)
            {
                this.height = height;
                this.width = width;
                table = SimulationTableAPI.GenerateBoard(height, width, colour);
                _threads = new List<Thread>();
            }

            public override void AddBall(BallDataAPI ball)
            {
                lock (_lock) table.AddBall(ball);
            }

            public override List<BallDataAPI> GetBalls()
            {
                return table.GetBalls();
            }

            public override void ClearBalls()
            {
                table.ClearBalls();
            }

            public override void StartMovingBalls()
            {
                _active = true;
                foreach (var ball in table.GetBalls())
                {
                    Thread thread = new Thread(() =>
                    {
                        while (_active)
                        {
                            lock (_lock) ball.UpdateBall(width, height);
                            Thread.Sleep(10);
                        }
                    })
                    { IsBackground = true };

                    _threads.Add(thread);
                    thread.Start();
                }
            }

            public override void StopMovingBalls()
            {
                _active = false;
                foreach (var thread in _threads) thread.Join();
            }
        }
    }
}