using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.Domain.DataItemAggregate
{
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
        public  string Key { get; set; }
      
        /// <summary>
        /// 值
        /// </summary>
        public  string Value { get; set; }
    }

    public interface IStaticDataItemManager
    {
        void Add(List<DataItemDto> dataItemDtos);

        void Add(DataItemDto dataItemDto);

        void Update(DataItemDto dataItemDto);

        void Remove(DataItemDto dataItemDto);

        List<DataItemDto> GetList(string category);

        DataItemDto Get(string category,string key);
    }
}
