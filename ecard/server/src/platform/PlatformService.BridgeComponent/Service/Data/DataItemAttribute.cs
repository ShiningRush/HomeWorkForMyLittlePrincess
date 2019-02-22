using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Service.Data
{
    /// <summary>
    /// 数据字典转换
    /// </summary>
    public class DataItemAttribute : Attribute
    {
        public string ItemCode { get; set; }

        public DataItemAttribute(string itemCode = "")
        {
            ItemCode = itemCode;
        }
    }
}
