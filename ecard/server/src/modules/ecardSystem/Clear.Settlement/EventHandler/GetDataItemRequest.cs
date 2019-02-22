using ServiceAnt.Handler;
using ServiceAnt.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.Settlement.EventHandler
{
    /// <summary>
    /// 请求数据字典
    /// </summary>
    public class GetDataItemRequest : IRequestTrigger
    {
        public string[] Categorys { get; set; }
    }

    public class DataItemDto
    {
        public DataItemDto() { }
        public DataItemDto(string category, string key, string value)
        {
            Category = category;
            Key = key;
            Value = value;
        }
        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
