using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
        public abstract void StartMovingBalls();
        public abstract void StopMovingBalls();
        public abstract List<BallDataAPI> GetBalls();
        public abstract void ClearBalls();

        private class DataManager : DataManagerAPI
        {
            private List<Thread> _threads;
            private bool _active = false;
            private object _lock = new object();
            private SimulationTableAPI table;
            private double height;
            private double width;

            public DataManager(double height, double width, string colour)
            {
                this.table = SimulationTableAPI.GenerateBoard(height, width, colour);
                this.height = height;
                this.width = width;
                this._threads = new();


            }

            public override void AddBall(BallDataAPI ball)
            {
                lock (_lock)
                {
                    table.AddBall(ball);
                }
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

                            lock (_lock)
                            {
                                ball.UpdateBall(width, height);
                            }
                            Thread.Sleep(10);
                        }
                    })
                    {
                        IsBackground = true
                    };
                    _threads.Add(thread);
                    thread.Start();
                }
            }

            public override void StopMovingBalls()
            {
                _active = false;
                foreach (var thread in _threads)
                {
                    thread.Join();
                }
            }

            public override List<BallDataAPI> GetBalls()
            {
                return table.GetBalls();
            }

            public override void ClearBalls()
            {
                table.ClearBalls();
            }

        }
    }
}
