using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapPolygonWriter.Core
{
    public class Coordinate
    {
        double Latitude { set; get; }
        double Longitude { set; get; }

        public Coordinate(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public override string ToString()
        {
            return $"[{Latitude}:{Longitude}]";
        }

        public static implicit operator Tuple<double,double>(Coordinate c)
        {
            return new Tuple<double, double>(c.Latitude, c.Longitude);
        }

        public static implicit operator Coordinate(Tuple<double, double> t)
        {
            return new Coordinate(t.Item1, t.Item2);
        }
    }
}
