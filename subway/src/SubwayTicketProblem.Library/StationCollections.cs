using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayTicketProblem.Library
{

    public class Station
    {
        public string Name { get; set; }
        public int Distance { get; set; }
    }

    public static class StationCollection
    {
        /// <summary>
        /// 返回定义的所有地铁站
        /// </summary>
        /// <returns></returns>
        public static List<Station> AllStation()
        {
            return new List<Station>
            {
                new Station{ Name = "国贸", Distance = 0 },
                new Station{ Name = "老街", Distance = 4 },
                new Station{ Name = "大剧院", Distance = 8 },
                new Station{ Name = "科学馆", Distance = 12 },
                new Station{ Name = "华强路", Distance = 14 },
                new Station{ Name = "岗厦", Distance = 18 },
                new Station{ Name = "购物中心", Distance = 22 },
                new Station{ Name = "会展中心", Distance = 27 }
            };
        }

        /// <summary>
        /// 根据站名获取地铁站
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public static Station GetStationByName(string stationName)
        {
            return AllStation().FirstOrDefault(p => p.Name == stationName);
        }
    }
}
