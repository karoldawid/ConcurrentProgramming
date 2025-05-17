namespace Data;
using System.Collections.Generic;
using System.ComponentModel;

public abstract class SimulationTableAPI
{
    public static SimulationTableAPI GenerateBoard(double tableHeight, double tableWidth, string colour)
    {
        return new SimulationTable(tableHeight, tableWidth, colour);
    }

    public abstract double _tableWidth { get; }
    public abstract double _tableHeight { get; }
    public abstract string _colour { get; }
    public abstract List<BallDataAPI> GetBalls();
    public abstract void AddBall(BallDataAPI ball);
    public abstract void ClearBalls();
    public abstract event PropertyChangedEventHandler PropertyChanged;
}

public class SimulationTable : SimulationTableAPI, INotifyPropertyChanged
{
    private readonly List<BallDataAPI> balls;
    private readonly double tableHeight;
    private readonly double tableWidth;
    private readonly string colour;

    public SimulationTable(double tableHeight, double tableWidth, string colour)
    {
        this.tableHeight = tableHeight;
        this.tableWidth = tableWidth;
        this.colour = colour;
        balls = new List<BallDataAPI>();
    }

    public override double _tableHeight => tableHeight;
    public override double _tableWidth => tableWidth;
    public override string _colour => colour;

    public override List<BallDataAPI> GetBalls() => balls;

    public override void AddBall(BallDataAPI ball)
    {
        balls.Add(ball);
    }

    public override void ClearBalls()
    {
        balls.Clear();
    }

    public override event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}