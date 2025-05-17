using Logic;
using Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Presentation.Model
{
    public abstract class MainAPI
    {
        public static MainAPI GenerateTable(double tableWidth, double tableHeight) => new Main(tableWidth, tableHeight);

        public abstract ObservableCollection<BallModelAPI> Balls { get; set; }
        public abstract void Update();
        public abstract void GenerateBalls(int amount);
        public abstract void ClearMap();
        public abstract void PauseMovement();
        public abstract List<BallDataAPI> GetBalls();

        private class Main : MainAPI
        {
            private readonly double tableWidth;
            private readonly double tableHeight;
            private readonly BallLogicAPI ballLogic;
            private ObservableCollection<BallModelAPI> _ballModel = new ObservableCollection<BallModelAPI>();

            public Main(double tableWidth, double tableHeight)
            {
                this.tableWidth = tableWidth;
                this.tableHeight = tableHeight;
                ballLogic = BallLogicAPI.GenerateLogic(tableWidth, tableHeight);
            }

            public override ObservableCollection<BallModelAPI> Balls
            {
                get => _ballModel;
                set => _ballModel = value;
            }

            public override List<BallDataAPI> GetBalls() => ballLogic.GetBalls();

            public override void Update() => ballLogic.RunAnimation();

            public override void PauseMovement() => ballLogic.PauseAnimation();

            public override void GenerateBalls(int amount)
            {
                ballLogic.GenerateBallSet(amount);
                _ballModel.Clear();
                foreach (var ball in ballLogic.GetBalls())
                {
                    _ballModel.Add(BallModelAPI.GenerateBallModel(ball));
                }
            }

            public override void ClearMap()
            {
                ballLogic.ClearTable();
                _ballModel.Clear();
            }
        }
    }
}