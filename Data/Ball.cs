namespace Data;
public abstract class BallAPI{
    public static BallAPI Generate(int ID, double X, double Y, double radius, string color, int XDirection, int YDirection){
        return new Ball(ID,  X,  Y, radius, color, XDirection, YDirection);
    }

    public int ID { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Radius { get; set; }
    public string Color { get; set; }
    public int XDirection { get; set; }
    public int YDirection { get; set; }

    private class Ball : BallAPI
    {
        public Ball(int ID, double X, double Y, double radius, string color, int XDirection, int YDirection)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
            this.Color = color;
            this.Radius = radius;
            this.XDirection = XDirection;
            this.YDirection = YDirection;
            
        }

    }

    }