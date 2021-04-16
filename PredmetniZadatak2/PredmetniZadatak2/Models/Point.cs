using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredmetniZadatak2.Models
{
    public class Point
    {
        private double pointX;
        private double pointY;


        public double PointY
        {
            get { return pointY; }
            set { pointY = value; }
        }
        public double PointX
        {
            get { return pointX; }
            set { pointX = value; }
        }


        public Point() { }
    }
}
