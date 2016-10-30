using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWebsiteInteration.RecordQuery.Resaults
{
    public class ShipInfo
    {
        /// <summary>
        /// 击落的飞机
        /// </summary>
        public int KilledPlane;
        /// <summary>
        /// 团队作战次数
        /// </summary>
        public int TeamBattles;
        /// <summary>
        /// 团队胜利次数
        /// </summary>
        public int TeamWins;
        /// <summary>
        /// 船的Cd
        /// </summary>
        public string VehicleTypeCd;
        /// <summary>
        /// 战斗类型
        /// </summary>
        public int BattleTypeId;
        /// <summary>
        /// 击沉的船
        /// </summary>
        public int KilledShip;
        /// <summary>
        /// 对巡洋舰造成的伤害
        /// </summary>
        public int DamagedCV;
        /// <summary>
        /// 对驱逐舰造成的伤害
        /// </summary>
        public int DamagedDD;
        /// <summary>
        /// 最大伤害
        /// </summary>
        public int MaxDamage;
        /// <summary>
        /// 团队战力
        /// </summary>
        public double TeamPower;
        /// <summary>
        /// 总战力
        /// </summary>
        public double TotalPower;
        /// <summary>
        /// 总伤害
        /// </summary>
        public int Damage;
        /// <summary>
        /// 有效激活日期
        /// </summary>
        public DateTime ActiveDate;
        /// <summary>
        /// 最高击落
        /// </summary>
        public int MaxKilledPlane;
        /// <summary>
        /// 对战列舰造成的伤害
        /// </summary>
        public int DamagedBB;
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime UpdateDate;
        /// <summary>
        /// 最高击沉
        /// </summary>
        public int MaxKilledShip;
        /// <summary>
        /// 场次
        /// </summary>
        public int Battles;
        /// <summary>
        /// 胜利次数
        /// </summary>
        public int wins;
    }
}
