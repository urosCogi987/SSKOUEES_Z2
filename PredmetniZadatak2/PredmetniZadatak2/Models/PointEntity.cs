using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PredmetniZadatak2.Models
{
    public class PointEntity
    {
        private long id;
        private string name;
        private string toolTip;
        private Brush brush;
        private double pointX;
        private double pointY;


        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string ToolTip
        {
            get { return toolTip; }
            set { toolTip = value; }
        }
        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }
        public double PointX
        {
            get { return pointX; }
            set { pointX = value; }
        }

        public double PointY
        {
            get { return pointY; }
            set { pointY = value; }
        }
    }
}
