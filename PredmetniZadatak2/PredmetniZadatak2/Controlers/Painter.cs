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
        public static void DrawEntities(PointEntity entity, Brush color, Point point, Canvas mapCanvas)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = 8;
            ellipse.Width = 8;
            ellipse.Fill = color;

            
        }
    }
}
