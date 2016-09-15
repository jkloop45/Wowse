using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using WWebsiteInteration.RecordQuery.Resaults;

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
        public Task<QueryResult> LoadRecord()
        {
            var urlName = HttpUtility.UrlEncode(Username);
            var zone = IsSouth ? "south" : "north";
            var json = WebInteraction.GET(GET_LOGIN_URL + "name=" + urlName + "&zone=" + zone);

            // "account_db_id":"1837225125"
            Aid = Regex.Match(json, @"""account_db_id"":""\d{10}""").Value;

            var shipInfo = WebInteraction.GET(SHIP_INFO_URL + "aid=" + Aid);
            
        }
    }
}
