using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CuisineCompanion.Common;

public class Sector : Shape, INotifyPropertyChanged
{
    private readonly ArcSegment path1A;
    private readonly LineSegment path1B;
    private readonly ArcSegment path1C;
    private readonly LineSegment path1D;

    private readonly ArcSegment path2A;
    private readonly LineSegment path2B;
    private readonly ArcSegment path2C;
    private readonly LineSegment path2D;
    private readonly PathFigure pathFigure1;
    private readonly PathFigure pathFigure2;

    //扇形角度
    private double _angle;

    //中心点
    private Point _center;

    //内径
    private double _innerRadius;

    private bool _isStroked;

    //外径
    private double _outerRadius;

    //开始角度
    private double _startAngle;
    private double _x;
    private double _y;

    public Sector()
    {
        path1A = new ArcSegment
        {
            RotationAngle = 0,
            IsLargeArc = false,
            SweepDirection = SweepDirection.Counterclockwise
        };

        path1B = new LineSegment();

        path1C = new ArcSegment
        {
            RotationAngle = 0,
            IsLargeArc = false,
            SweepDirection = SweepDirection.Clockwise
        };

        path1D = new LineSegment();

        pathFigure1 = new PathFigure
        {
            Segments = new PathSegmentCollection
            {
                path1A, path1B, path1C, path1D
            }
        };

        path2A = new ArcSegment
        {
            RotationAngle = 0,
            IsLargeArc = false,
            SweepDirection = SweepDirection.Counterclockwise
        };

        path2B = new LineSegment();

        path2C = new ArcSegment
        {
            RotationAngle = 0,
            IsLargeArc = false,
            SweepDirection = SweepDirection.Clockwise
        };

        path2D = new LineSegment();

        pathFigure2 = new PathFigure
        {
            Segments = new PathSegmentCollection
            {
                path2A, path2B, path2C, path2D
            }
        };

        DefiningGeometry = new PathGeometry
        {
            Figures = new PathFigureCollection
            {
                pathFigure1, pathFigure2
            }
        };
        Init();
        // Fill = Brushes.LightBlue;
    }

    public bool IsStroked
    {
        get => _isStroked;
        set
        {
            _isStroked = value;
            OnPropertyChanged(nameof(IsStroked));
            path1A.IsStroked = value;
            path1B.IsStroked = value;
            path1C.IsStroked = value;
            path1D.IsStroked = value;
            path2A.IsStroked = value;
            path2B.IsStroked = value;
            path2C.IsStroked = value;
            path2D.IsStroked = value;
        }
    }

    protected override Geometry DefiningGeometry { get; }


    public double X
    {
        get => _x;
        set
        {
            _x = value;
            OnPropertyChanged(nameof(X));
            Center = new Point(_x, _y);
        }
    }

    public double Y
    {
        get => _y;
        set
        {
            _y = value;
            OnPropertyChanged(nameof(Y));
            Center = new Point(_x, _y);
        }
    }

    public Point Center
    {
        get => _center;
        set
        {
            _center = value;
            OnPropertyChanged(nameof(Center));
            DrawSector();
        }
    }

    public double InnerRadius
    {
        get => _innerRadius;
        set
        {
            value = Math.Max(0, value);
            _outerRadius = Math.Max(_outerRadius, value);
            _innerRadius = value;
            OnPropertyChanged(nameof(InnerRadius));
            DrawSector();
        }
    }

    public double OuterRadius
    {
        get => _outerRadius;
        set
        {
            value = Math.Max(0, value);
            _innerRadius = Math.Min(_innerRadius, value);
            _outerRadius = value;
            OnPropertyChanged(nameof(OuterRadius));
            DrawSector();
        }
    }

    public double StartAngle
    {
        get => _startAngle;
        set
        {
            _startAngle = value;
            OnPropertyChanged(nameof(StartAngle));
            DrawSector();
        }
    }

    public double Angle
    {
        get => _angle;
        set
        {
            _angle = value;
            OnPropertyChanged(nameof(Angle));
            DrawSector();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void Init()
    {
        _center = new Point(200, 200);
        _outerRadius = 30;
        _startAngle = 30;
        _angle = 30;
        IsStroked = true;
        Fill = Brushes.LightBlue;
        DrawSector();
    }

    private void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private static double AngleCorrection(double angle)
    {
        angle = Math.Max(0, angle);
        if (angle < 0.0001) return 0;

        angle %= 360;
        if (angle < 0.0001) return 360;

        return angle;
    }


    private void DrawSector()
    {
        _startAngle = AngleCorrection(_startAngle);
        _angle = AngleCorrection(_angle);
        var midAngle = AngleCorrection(_startAngle + _angle / 2);
        var endAngle = AngleCorrection(_startAngle + _angle);

        // 计算扇形的起点和终点
        var outerStartPoint = CalculateArcPoint(_center, _outerRadius, _startAngle);
        var outerEndPoint = CalculateArcPoint(_center, _outerRadius, endAngle);

        var innerStartPoint = CalculateArcPoint(_center, _innerRadius, _startAngle);
        var innerEndPoint = CalculateArcPoint(_center, _innerRadius, endAngle);

        var outerMidPoint = CalculateArcPoint(_center, _outerRadius, midAngle);
        var innerMidPoint = CalculateArcPoint(_center, _innerRadius, midAngle);

        path1A.Point = innerMidPoint;
        path1B.Point = outerMidPoint;
        path1C.Point = outerStartPoint;
        path1D.Point = innerStartPoint;

        path2A.Point = innerEndPoint;
        path2B.Point = outerEndPoint;
        path2C.Point = outerMidPoint;
        path2D.Point = innerMidPoint;

        pathFigure1.StartPoint = innerStartPoint;
        pathFigure2.StartPoint = innerMidPoint;

        path1A.Size = path2A.Size = new Size(_innerRadius, _innerRadius);
        path1C.Size = path2C.Size = new Size(_outerRadius, _outerRadius);
    }


    /// <summary>
    ///     计算出弧线上的某点
    /// </summary>
    /// <param name="center">中心点</param>
    /// <param name="radius">半径</param>
    /// <param name="angle">角度</param>
    /// <returns>弧线上的某点</returns>
    private static Point CalculateArcPoint(Point center, double radius, double angle)
    {
        // 将角度转换为弧度
        var rad = angle * (Math.PI / 180);

        var x = center.X + radius * Math.Cos(rad);
        var y = center.Y - radius * Math.Sin(rad);

        return new Point(x, y);
    }
}