using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace WWebsiteInteration.RecordQuery
{
    static public class Ships
    {
        const string SHIP_INFO_URL = @"http://rank.kongzhong.com/wows/scripts/ships.js";
        static public Dictionary<string, Ship> GetShips()
        {
            var result = new Dictionary<string, Ship>();
            var shipInfo = WebInteraction.GET(SHIP_INFO_URL);
            shipInfo = shipInfo.Substring(13);
            shipInfo = shipInfo.Replace(@"'", @"""");

            var reader = new JsonTextReader(new StringReader(shipInfo));
            while (reader.Read())
            {
                if (reader.Path != string.Empty && Regex.Match(reader.Path, @"\d{10}").Value == reader.Path)
                {
                    if(!result.ContainsKey(reader.Path))
                    {
                        var ship = new Ship(reader.Path);
                        result.Add(ship.Cd, ship);
                    }
                    continue;
                }
                var match = Regex.Match(reader.Path, @"(\d{10})\.(.+)");
                switch (match.Groups[2].Value)
                {
                    case "name":
                        result[match.Groups[1].Value].Name = reader.ReadAsString();
                        break;
                    case "alias":
                        result[match.Groups[1].Value].Alias = reader.ReadAsString();
                        break;
                    case "country":
                        result[match.Groups[1].Value].Country = reader.ReadAsString();
                        break;
                    case "type":
                        result[match.Groups[1].Value].Type = reader.ReadAsString();
                        break;
                    case "lvl":
                        result[match.Groups[1].Value].Lvl = reader.ReadAsInt32().Value;
                        break;
                }
            }
            return result;
        }
    }
}
