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
    static class Ships
    {
        const string SHIP_INFO_URL = @"http://rank.kongzhong.com/wows/scripts/ships.js";
        static Dictionary<string, Ship> GetShips()
        {
            var result = new Dictionary<string, Ship>();
            var shipInfo = WebInteraction.GET(SHIP_INFO_URL);
            shipInfo = shipInfo.Substring(14);
            shipInfo = shipInfo.Replace(@"'", @"""");

            var reader = new JsonTextReader(new StringReader(shipInfo));
            while (reader.Read())
            {
                if (Regex.Match(reader.Path, @"\d{10}").Value == reader.Path)
                {
                    var ship = new Ship(reader.Path);
                    reader.Read();
                    while (reader.Path.Substring(0, 10) == ship.Cd)
                    {
                        var s = reader.Path.Split('\\');
                        switch (s[s.Length-1])
                        {
                            case "name":
                                ship.Name = reader.ReadAsString();
                                break;
                            case "alias":
                                ship.Alias = reader.ReadAsString();
                                break;
                            case "country":
                                ship.Country = reader.ReadAsString();
                                break;
                            case "type":
                                ship.Type = reader.ReadAsString();
                                break;
                            case "lvl":
                                ship.Lvl = reader.ReadAsInt32();
                                break;
                        }
                    }
                    result.Add(reader.Path, ship);
                }
            }
        }
    }
}
