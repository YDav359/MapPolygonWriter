using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MapPolygonWriter.Core.MapServices
{
    public class OsmMapService : IMapService
    {
        public IEnumerable<Coordinate> GetData(string address)
        {
            var osm = GetOsmResult(address);
            var points = GetPoints(osm);

            return points;
        }

        private string GetOsmResult(string address)
        {
            var url = "https://nominatim.openstreetmap.org/search?format=json&q=";
            var encodeAddress = WebUtility.UrlEncode(address);
            var req = WebRequest.CreateHttp($"{url}{encodeAddress}&polygon_geojson=1");
            req.UserAgent = ".NET Framework Test Client";
            req.Method = "GET";
            var res = req.GetResponse();
            string result = string.Empty;
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        private List<Coordinate> GetPoints(string raw)
        {
            var re = new Regex(@"\[\[.+?\]\]");
            var m = re.Match(raw);

            var resultStr = m.Value;
            re = new Regex(@"\[(\d+\.\d+),(\d+\.\d+)\]");
            var matches = re.Matches(resultStr);

            var resultList = new List<Coordinate>();
            foreach (Match item in matches)
            {
                var x = double.Parse(item.Groups[1].Value.Replace('.', ','));
                var y = double.Parse(item.Groups[2].Value.Replace('.', ','));
                resultList.Add(new Coordinate(x, y));
            }

            return resultList;
        }
    }
}
