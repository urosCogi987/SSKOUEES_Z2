using PredmetniZadatak2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PredmetniZadatak2.Controlers
{
    public class Calculator
    {
        private static Dictionary<long, Point> entitiesOnCanvas = new Dictionary<long, Point>();
        private static HashSet<PointEntity> pointEntities = new HashSet<PointEntity>();           


        public static void CalculateMinMax(HashSet<PointEntity> entities, out double maxLatitude,  out double minLatitude,  out double maxLongitude,  out double minLongitude)
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

            pointEntities = entities;
        }

        public static Point GetCoordinates(PointEntity entity, double maxLatitude, double minLatitude, double maxLongitude, double minLongitude, double width, double height)
        {
            double valOfSingleLongitude = (maxLongitude - minLongitude) / width;      // pravimo 3000 delova(Longituda) jer nam je canvas 3000 x 3000
            double valOfSingleLatitude = (maxLatitude - minLatitude) / height;
            
            double x = Math.Round((entity.Longitude - minLongitude) / valOfSingleLongitude);    // koliko longituda stane u rastojanje izmedju trenutne i minimalne longitude
            double y = Math.Round((maxLatitude - entity.Latitude) / valOfSingleLatitude);

            x = x - (x % 10);   // zaokruzi na prvi broj deljiv sa 10, toliko ce nam biti rastojanje izmedju dva susedna x
            y = y - (y % 10);

            int counter = 0;

            while (true)
            {
                if (entitiesOnCanvas.Any(item => item.Value.X == x && item.Value.Y == y))
                {
                    if (counter == 0)
                    {
                        x += 10;
                        counter++;
                        continue;
                    }
                    else if (counter == 1)
                    {
                        x -= 20;
                        counter++;
                        continue;
                    }
                    else if (counter == 2)
                    {
                        x += 10; //vraxamo x na pocetnu vrednost
                        y += 10;
                        counter++;
                        continue;
                    }
                    else if (counter == 3)
                    {
                        y -= 20;
                        counter++;
                        continue;
                    }
                    else if (counter == 4)
                    {
                        x += 10;
                        counter++;
                        continue;
                    }
                    else if (counter == 5)
                    {
                        x -= 20;
                        counter++;
                        continue;
                    }
                    else if (counter == 6)
                    {
                        y += 20;
                        counter++;
                        continue;
                    }
                    else if (counter == 6)
                    {
                        x += 20;
                        counter++;
                        continue;
                    }
                    else
                    {
                        counter = 0;
                        continue;
                    }
                }
                else
                {
                    break;
                }
            }
            

            Point p = new Point(x, y);
            entitiesOnCanvas.Add(entity.Id, p);

            return p;
        }        
    }
}
