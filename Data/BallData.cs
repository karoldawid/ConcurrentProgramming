namespace Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public abstract class BallDataAPI
{
    public static BallDataAPI GenerateBall(int ID, double X, double Y, double r, string colour, int dirX, int dirY, double mass)
    {
        return new BallData(ID, X, Y, r, colour, dirX, dirY, mass);
    }

    public int ID { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double r { get; set; }
    public string colour { get; set; }
    public int dirX { get; set; }
    public int dirY { get; set; }
    public double mass { get; set; }

    private bool _active = true;
    public abstract double velX { get; set; }
    public abstract double velY { get; set; }
    public abstract double posX { get; set; }
    public abstract double posY { get; set; }
    public abstract bool IsActive { get; set; }
    public abstract void UpdateBall(double tableWidth, double tableHeight);

    public abstract event PropertyChangedEventHandler PropertyChanged;

    private class BallData : BallDataAPI, INotifyPropertyChanged
    {
        private double _velX;
        private double _velY;
        private readonly object _syncObject = new object();
        public BallData(int ID, double X, double Y, double r, string colour, int dirX, int dirY, double mass)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
            this.colour = colour;
            this.r = r;
            this.dirX = dirX;
            this.mass = mass;
            this.dirY = dirY;
            this._velX = 1;
            this._velY = 1;
        }

        public override double velX
        {
            get => _velX;
            set
            {
                _velX = value;
                OnPropertyChanged(nameof(velX));
            }
        }
        public override double velY
        {
            get => _velY;
            set
            {
                _velY = value;
                OnPropertyChanged(nameof(velY));
            }
        }
        public override double posX
        {
            get => X;
            set
            {
                if (X != value)
                {
                    X = value;
                    OnPropertyChanged(nameof(posX));
                }
            }
        }
        public override double posY
        {
            get => Y;
            set
            {
                if (Y != value)
                {
                    Y = value;
                    OnPropertyChanged(nameof(posY));
                }
            }
        }
        public override void UpdateBall(double tableWidth, double tableHeight)
        {
            lock (_syncObject)
            {
                if (posX + velX * dirX < 0 || posX + velX * dirX > tableWidth)
                    dirX = -dirX;

                if (posY + velY * dirY < 0 || posY + velY * dirY > tableHeight)
                    dirY = -dirY;

                posX += velX * dirX;
                posY += velY * dirY;
            }
        }
        public override bool IsActive
        {
            get => _active;
            set
            {
                _active = value;
                OnPropertyChanged(nameof(IsActive));

            }
        }

        public override event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}