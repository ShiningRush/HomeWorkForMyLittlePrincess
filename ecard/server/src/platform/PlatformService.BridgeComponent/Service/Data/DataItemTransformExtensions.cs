using Abp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.BridgeComponent.Service.Data
{
   public static class DataItemTransformExtensions
    {
        public static bool HasDataItem(this object obj)
        {
            if (obj == null) return false;
            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                var propertyValue = propertyInfo.GetValue(obj);
                if (!(propertyValue is string)) continue;
                var customAttribute = propertyInfo.GetCustomAttributes(typeof(DataItemAttribute), false).FirstOrDefault();
                if (customAttribute != null) return true;
            }
            return false;
        }

        public static void LoopDataItem(this object obj,Action<string, string> action)
        {
            if (obj == null || obj.GetType().IsValueType) return ;
            if (obj is IEnumerable)
            {
                foreach (var item in (obj as IEnumerable))
                {
                    if (!HasDataItem(item))
                    {
                        return ;
                    }
                    LoopDataItem(item, action);
                }
            }
            else
            {
                foreach (var propertyInfo in obj.GetType().GetProperties())
                {
                    var propertyValue = propertyInfo.GetValue(obj);

                    var customAttribute = propertyInfo.GetCustomAttributes(typeof(DataItemAttribute), false).FirstOrDefault();
                    if (customAttribute == null) continue;
                    if ((propertyValue is string))
                    {
                        string itemCode = (customAttribute as DataItemAttribute).ItemCode;
                        if (itemCode.IsNullOrEmpty())
                        {
                            itemCode = propertyInfo.Name;
                        }
                        action(itemCode, propertyValue.ToString());
                    }
                    else if (!propertyInfo.PropertyType.IsValueType)
                    {
                        LoopDataItem(propertyValue, action);
                    }
                }
            }
        }

        public static void DataItemTransform(this object obj,Func<string,string,string> GetDataItemValue)
        {
            if (obj == null || obj.GetType().IsValueType) return;
            if (obj is IEnumerable)
            {
                foreach (var item in (obj as IEnumerable))
                {
                    if (!HasDataItem(item))
                    {
                        return;
                    }
                    DataItemTransform(item, GetDataItemValue);
                }
            }
            else
            {
                foreach (var propertyInfo in obj.GetType().GetProperties())
                {
                    var propertyValue = propertyInfo.GetValue(obj);

                    var customAttribute = propertyInfo.GetCustomAttributes(typeof(DataItemAttribute), false).FirstOrDefault();
                    if (customAttribute == null) continue;
                    if ((propertyValue is string))
                    {
                        string itemCode = (customAttribute as DataItemAttribute).ItemCode;
                        if (itemCode.IsNullOrEmpty())
                        {
                            itemCode = propertyInfo.Name;
                        }
                        string dataItemValue = GetDataItemValue(itemCode, propertyValue.ToString());
                        if (dataItemValue.IsNullOrEmpty())
                        {
                            dataItemValue = propertyValue.ToString();
                        }
                        var displayProperty = GetDisplayProperty(obj.GetType(), propertyInfo);
                        displayProperty.SetValue(obj, dataItemValue);
                    }
                    else if (!propertyInfo.PropertyType.IsValueType)
                    {
                        DataItemTransform(propertyValue, GetDataItemValue);
                    }
                }
            }
        }
        /// <summary>
        /// 获取显示属性名
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetDisplayPropertyName(string key)
        {
            return $"{key}Name";
        }
        /// <summary>
        /// 获取显示属性
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static PropertyInfo GetDisplayProperty(Type type, PropertyInfo currentPropertyInfo)
        {
            return type.GetProperty(GetDisplayPropertyName(currentPropertyInfo.Name)) ?? currentPropertyInfo;
        }
    }
}
