﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PredmetniZadatak2.Models
{
    public class NodeEntity : PointEntity
    {
        private Brush color;
        public Brush Color
        {
            get { return color; }
            set { color = value; }
        }


        public NodeEntity()
        {
            Color = Brushes.Blue;
        }
    }
}
