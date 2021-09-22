using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapPolygonWriter.Core.MapServices
{
    public interface IMapService
    {
        IEnumerable<Coordinate> GetData(string address);
    }
}
