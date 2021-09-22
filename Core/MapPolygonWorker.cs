using MapPolygonWriter.Core.MapServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapPolygonWriter.Core
{
    public class MapPolygonWorker
    {
        IMapService _mapService;

        public MapPolygonWorker(IMapService mapService)
        {
            _mapService = mapService;
        }

        private IEnumerable<Coordinate> GetCoordinates(string address, int step)
        {
            var cords = _mapService.GetData(address);

            if (step <= 1) return cords;

            var buffer = new List<Coordinate>();
            int n = 1;
            foreach (var item in cords)
            {
                if (n % step == 0) buffer.Add(item);
                n++;
            }

            return buffer;
        }

        public void UnloadPolygon(string address, string path, int step)
        {
            var cords = GetCoordinates(address, step);

            File.WriteAllLines(path, cords.Select(x => x.ToString()));
        }
    }
}
