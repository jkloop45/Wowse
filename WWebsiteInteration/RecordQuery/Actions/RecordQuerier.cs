using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using WWebsiteInteration.RecordQuery.Resaults;
using System.IO;
using Logcat;

namespace WWebsiteInteration.RecordQuery.Actions
{
    public class RecordQuerier
    {
        const string GET_LOGIN_URL = @"http://rank.kongzhong.com/Data/action/WowsAction/getLogin?";
        const string SHIP_INFO_URL = @"http://rank.kongzhong.com/Data/action/WowsAction/getShipInfo?";

        public string Username { get; internal set; }
        public bool IsSouth { get; internal set; }
        private string Aid { get; set; }

        public RecordQuerier(string username, bool isSouth)
        {
            Username = username;
            IsSouth = isSouth;
        }
        public QueryResult LoadRecord()
        {
            var result = new QueryResult();

            var urlName = HttpUtility.UrlEncode(Username);
            var zone = IsSouth ? "south" : "north";
            var json = WebInteraction.GET(GET_LOGIN_URL + "name=" + urlName + "&zone=" + zone);

            // "account_db_id":"1837225125"
            if(json == @"{""errno"":2}")
            {
                throw new Exception("UserNotExits");
            }
            Aid = Regex.Match(json, @"(?<=""account_db_id"":"")\d{10}(?="")").Value;

            var shipInfo = WebInteraction.GET(SHIP_INFO_URL + "aid=" + Aid);
            var ships = Ships.GetShips();

            //var shipInfos = new Dictionary<int, ShipInfo>();
            var shipCounts = new int[10];
            var shipPov = new int[10];

            var reader = new JsonTextReader(new StringReader(shipInfo));
            string thisId = "";

            while (reader.Read())
            {
                var match = Regex.Match(reader.Path, @"\[(\d+?)]\.(.+)");
                if (match.Groups[2].Value == "id.vehicleTypeCd")
                {
                    thisId = reader.ReadAsString();
                }
                if (match.Groups[2].Value == "battles")
                {
                    var lvl = ships[thisId].Lvl;
                    var type = ships[thisId].Type;
                    var battles = reader.ReadAsInt32().Value;
                    shipCounts[--lvl] += battles;
                    switch (type)
                    {
                        case "Cruiser":
                            result.CA+= battles;
                            break;
                        case "AirCarrier":
                            result.CV+= battles;
                            break;
                        case "Destroyer":
                            result.DD+= battles;
                            break;
                        case "Battleship":
                            result.BB+= battles;
                            break;
                    }
                }
                if (match.Groups[2].Value == "wins")
                {
                    var lvl = ships[thisId].Lvl;
                    shipPov[--lvl] += reader.ReadAsInt32().Value;
                }
            }

            result.LvlShipCount = shipCounts;
            result.LvlShipVicPer = shipPov;

            return result;
        }

        public async Task<QueryResult> LoadRecordAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                   return LoadRecord();
                }catch(Exception ex)
                {
                    Loger.Log(ex.ToString());
                    return null;
                }
            });
        }
    }
}
