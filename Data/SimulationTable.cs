namespace Data;
using System;
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
    public abstract event PropertyChangedEventHandler PropertyChanged;

    public abstract void AddBall(BallDataAPI ball);
    public abstract void ClearBalls();
    public abstract List<BallDataAPI> GetBalls();
}

public class SimulationTable : SimulationTableAPI, INotifyPropertyChanged
{
    private double tableHeight;
    private double tableWidth;
    private string colour;
    private List<BallDataAPI> balls;

    public SimulationTable(double tableHeight, double tableWidth, string colour)
    {
        this.tableHeight = tableHeight;
        this.tableWidth = tableWidth;
        this.colour = colour;
        this.balls = new List<BallDataAPI>();
    }

    public override void AddBall(BallDataAPI ball)
    {
        balls.Add(ball);
    }

    public override double _tableHeight => tableHeight;
    public override double _tableWidth => tableWidth;
    public override string _colour => colour;

    public override List<BallDataAPI> GetBalls()
    {
        return balls;
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

