using PredmetniZadatak2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredmetniZadatak2.Controlers
{
    public class Calculator
    {
        public static void CalculateMinMax(HashSet<PointEntity> entities, double maxLatitude, double minLatitude, double maxLongitude, double minLongitude)
        {
            maxLatitude = 0;
            maxLongitude = 0;
            minLatitude = 100;
            minLongitude = 100;

            foreach (PointEntity entity in entities)
            {
                if (maxLatitude < entity.Latitude)
                    maxLatitude = entity.Latitude;

                if (minLatitude > entity.Latitude)
                    minLatitude = entity.Latitude;

                if (maxLongitude < entity.Longitude)
                    maxLongitude = entity.Longitude;

                if (minLongitude > entity.Longitude)
                    minLongitude = entity.Longitude;
            }            
        }

        
    }
}
