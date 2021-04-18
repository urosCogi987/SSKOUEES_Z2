using PredmetniZadatak2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PredmetniZadatak2.Controlers
{
    public class Painter
    {
        enum LineType
        {
            Vertical,
            Horizontal
        }

        private static Dictionary<Point, LineType> horizontalLines = new Dictionary<Point, LineType>();
        private static Dictionary<Point, LineType> verticalLines = new Dictionary<Point, LineType>();

        public static void DrawPointEntities(PointEntity entity, Brush color, Point point, Canvas mapCanvas)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = 8;
            ellipse.Width = 8;
            ellipse.Fill = color;
            

            if (color == Brushes.Green)
            {
                ellipse.ToolTip = "Substation\nID: " + entity.Id.ToString() + "\nName: " + entity.Name;
            }                
            else if (color == Brushes.Blue) 
            {
                ellipse.ToolTip = "Node\nID: " + entity.Id.ToString() + "\nName: " + entity.Name;
            }                
            else
            {
                SwitchEntity switchEntity = (SwitchEntity)entity;
                ellipse.ToolTip = "Switch\nID: " + entity.Id + "\nName: " + entity.Name + "\nStatus: " + switchEntity.Status;    
            }
                

            mapCanvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, point.X);
            Canvas.SetTop(ellipse, point.Y);        
        }        

        public static void LineEntitiesCalculations(HashSet<LineEntity> lines, Canvas mapCanvas, Dictionary<long, Point> entitiesOnCanvas)
        {
            Point startPoint = new Point();
            Point endPoint = new Point();
            Point currentPoint = new Point();
            Point previousPoint = new Point();

            foreach (LineEntity line in lines)
            {
                if (!entitiesOnCanvas.ContainsKey(line.FirstEnd) || !entitiesOnCanvas.ContainsKey(line.SecondEnd))
                    continue;

                startPoint = entitiesOnCanvas[line.FirstEnd];
                endPoint = entitiesOnCanvas[line.SecondEnd];

                currentPoint.X = startPoint.X;
                currentPoint.Y = startPoint.Y;

                previousPoint.X = startPoint.X;
                previousPoint.Y = startPoint.Y;

                int step = (currentPoint.X > endPoint.X) ? -10 : 10;  // razmak izmedju dva x-a na gridu je 10, zbog toga i korak -10 ili +10
                while (currentPoint.X != endPoint.X) // crtamo po x
                {
                    currentPoint.X += step;
                    if (!horizontalLines.ContainsKey(new Point(currentPoint.X, currentPoint.Y)) || currentPoint.X == endPoint.X)
                    {
                        DrawLineEntities(line ,previousPoint, currentPoint, line.Color, mapCanvas);
                        horizontalLines[new Point(currentPoint.X, currentPoint.Y)] = LineType.Horizontal;
                    }
                    previousPoint.X = currentPoint.X;
                }

                step = (currentPoint.Y > endPoint.Y) ? -10 : 10;                
                while (currentPoint.Y != endPoint.Y)
                {
                    currentPoint.Y += step;
                    if (!verticalLines.ContainsKey(new Point(currentPoint.X, currentPoint.Y)) || currentPoint.Y == endPoint.Y)
                    {
                        DrawLineEntities(line, previousPoint, currentPoint, line.Color, mapCanvas);
                        verticalLines[new Point(currentPoint.X, currentPoint.Y)] = LineType.Vertical;
                    }
                    previousPoint.Y = currentPoint.Y;
                }
            }
            
            foreach (Point point in verticalLines.Keys)
            {
                if (horizontalLines.ContainsKey(new Point(point.X, point.Y)))
                {
                    Rectangle rekt = new Rectangle();

                    rekt.Height = 4;
                    rekt.Width = 4;
                    rekt.Fill = Brushes.Black;

                    mapCanvas.Children.Add(rekt);
                    Canvas.SetLeft(rekt, point.X + 2);
                    Canvas.SetTop(rekt, point.Y + 2);
                }
            }
        }

        public static void DrawLineEntities(LineEntity l, Point pointA, Point pointB, Brush color, Canvas mapCanvas)
        {
            Line line = new Line();

            line.Stroke = color;
            line.StrokeThickness = 2;
            line.X1 = pointA.X + 4; // +4 jer je sirina elipse(PointEntity) == 8
            line.Y1 = pointA.Y + 4;
            line.X2 = pointB.X + 4;
            line.Y2 = pointB.Y + 4;
            line.ToolTip = "Line\nID: " + l.Id.ToString() + "\nName: " + l.Name + "\nFirst end: " + l.FirstEnd.ToString() + "\nSecond end: " + l.SecondEnd.ToString();            

            mapCanvas.Children.Add(line);
        }
    }
}
