using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWebsiteInteration.RecordQuery.Resaults
{
    public class QueryResult
    {
        /// <summary>
        /// 排名
        /// </summary>
        public int Ranking = 0;
        /// <summary>
        /// 战列
        /// </summary>
        public int BB = 0;
        /// <summary>
        /// 驱逐
        /// </summary>
        public int DD = 0;
        /// <summary>
        /// 航妈
        /// </summary>
        public int CV = 0;
        /// <summary>
        /// 巡洋
        /// </summary>
        public int CA = 0;
        public int[] LvlShipCount = new int[10];
        public int[] LvlShipVicPer = new int[10];
    }
}
