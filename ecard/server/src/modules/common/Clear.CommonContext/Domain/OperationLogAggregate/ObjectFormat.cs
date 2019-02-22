using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.Domain.OperationLogAggregate
{
    /// <summary>
    /// 对象格式化成字符串
    /// </summary>
    public class ObjectFormat : System.ICustomFormatter, System.IFormatProvider
    {

        public static readonly ObjectFormat Instance = new ObjectFormat();

        //如果format Type与当前实例类型相同，则为当前实例，否则为空引用 
        public object GetFormat(Type format)
        {
            if (format == typeof(ICustomFormatter))
            {
                return this;
            }
            return null;
        }

        //实现Format方法说明: 
        //如果您的格式方法不支持格式，则确定正在设置格式的对象是否实现 IFormattable 接口。 
        //如果实现，请调用该接口的IFormattable.ToString 方法。 
        //否则，调用基础对象的默认 Object.ToString 方法。 
        public string Format(string format, object arg, IFormatProvider provider)
        {
            if(arg == null)
            {
                return null;
            }

            if (arg is IFormattable)
            {
                return ((IFormattable)arg).ToString(format, null);
            }
            else if (!arg.GetType().IsValueType)
            {
                if (!string.IsNullOrEmpty(format))
                {
                    string propertyName = format;
                    string newFormat = "";
                    var splitIndex = format.IndexOf(':');
                    if (splitIndex > 0)
                    {
                        propertyName = format.Substring(0, splitIndex);
                        newFormat = format.Substring(splitIndex + 1, format.Length - splitIndex - 1);
                    }
                    arg = arg.GetType().GetProperty(propertyName)?.GetValue(arg);
                    return Format(newFormat, arg, provider);
                }
            }
            return arg.ToString();
        }

    }
}
