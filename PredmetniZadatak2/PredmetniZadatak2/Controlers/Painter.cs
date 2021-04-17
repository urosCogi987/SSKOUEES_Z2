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
        public static void DrawPointEntities(PointEntity entity, Brush color, Point point, Canvas mapCanvas)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = 8;
            ellipse.Width = 8;
            ellipse.Fill = color;
            

            if (color == Brushes.Green)
            {
                ellipse.ToolTip = "Substation \nID: " + entity.Id + "\nName: " + entity.Name;
            }                
            else if (color == Brushes.Blue) 
            {
                ellipse.ToolTip = "Node \nID: " + entity.Id + "\nName: " + entity.Name;
            }                
            else
            {
                SwitchEntity switchEntity = (SwitchEntity)entity;
                ellipse.ToolTip = "Switch \nID: " + entity.Id + "\nName: " + entity.Name + "\nStatus: " + switchEntity.Status;    
            }
                

            mapCanvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, point.X);
            Canvas.SetTop(ellipse, point.Y);        
        }

        public static void DrawLineEnitites(HashSet<LineEntity> lines, Canvas mapCanvas, Dictionary<long, Point> entitiesOnCanvas)
        {
            //Point startNode = new Point();
            //Point endNode = new Point();
            //Point currentNode = new Point();
            //Point previousNode = new Point();

            //foreach (LineEntity line in lines)
            //{
            //    if (!entitiesOnCanvas.ContainsKey(line.FirstEnd) || !entitiesOnCanvas.ContainsKey(line.SecondEnd))                
            //        continue;
                

            //}
        }
    }
}
