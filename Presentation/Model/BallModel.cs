using Logic;
using Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Presentation.Model
{
    public abstract class BallModelAPI : INotifyPropertyChanged
    {
        public static BallModelAPI GenerateBallModel(BallDataAPI ball) => new BallModel(ball);

        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract double r { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;

        public class BallModel : BallModelAPI
        {
            private double _x;
            private double _y;
            private double _r;
            private PropertyChangedEventHandler _propertyChanged;

            public BallModel(BallDataAPI ball)
            {
                ball.PropertyChanged += BallPropertyChanged;
                X = ball.X;
                Y = ball.Y;
                r = ball.r;
            }

            public override double X
            {
                get => _x;
                set { _x = value; RaisePropertyChanged(); }
            }

            public override double Y
            {
                get => _y;
                set { _y = value; RaisePropertyChanged(); }
            }

            public override double r
            {
                get => _r;
                set { _r = value; RaisePropertyChanged(); }
            }

            public override event PropertyChangedEventHandler PropertyChanged
            {
                add => _propertyChanged += value;
                remove => _propertyChanged -= value;
            }

            private void BallPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                var ball = (BallDataAPI)sender;
                X = ball.X;
                Y = ball.Y;
            }

            private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            {
                _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}