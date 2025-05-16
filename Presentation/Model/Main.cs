using Logic;
using Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Presentation.Model
{
    public abstract class MainAPI
    {
        public static MainAPI GenerateTable(double tableWidth, double tableHeight)
        {
            return new Main(tableWidth, tableHeight);
        }

        public abstract void Update();
        public abstract List<BallDataAPI> GetBalls();
        public abstract void GenerateBalls(int amount);
        public abstract void ClearMap();
        public abstract void PauseMovement();
        private ObservableCollection<BallModelAPI> _ballModel = new ObservableCollection<BallModelAPI>();
        public ObservableCollection<BallModelAPI> Balls
        {
            get => _ballModel;
            set => _ballModel = value;
        }

        private class Main : MainAPI
        {
            private double tableWidth;
            private double tableHeight;

            private readonly BallLogicAPI ballLogic;

            public Main(double tableWidth, double tableHeight)
            {
                this.tableWidth = tableWidth;
                this.tableHeight = tableHeight;
                this.ballLogic = BallLogicAPI.GenerateLogic(tableWidth, tableHeight);
            }
            public override void Update()
            {
                this.ballLogic.RunAnimation();
            }

            public override void PauseMovement()
            {

                this.ballLogic.PauseAnimation();
            }

            public override List<BallDataAPI> GetBalls()
            {
                return this.ballLogic.GetBalls();
            }

            public override void GenerateBalls(int amount)
            {

                this.ballLogic.GenerateBallSet(amount);
                _ballModel.Clear();

                foreach (BallDataAPI ball in ballLogic.GetBalls())
                {
                    _ballModel.Add(BallModelAPI.GenerateBallModel(ball));
                }

            }

            public override void ClearMap()
            {
                this.ballLogic.ClearTable();
                _ballModel.Clear();

            }
        }
    }
}
