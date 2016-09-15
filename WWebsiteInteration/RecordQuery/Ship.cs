using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWebsiteInteration.RecordQuery
{
    public class Ship
    {
        public Ship(string cd)
        {
            this.cd = cd;
        }
        public string Cd { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public string Lvl { get; set; }
    }
}
