using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.CommonContext.Infrastructure.Extensions
{
    public static class JObjectExtension
    {
        public static object GetValueWithSplit(this JObject jObject,string nodeName,char split)
        {
            var names = nodeName.Split(split);
            JToken jToken = jObject;
            foreach (var name in names)
            {
                jToken = jToken[name];
            }
            return jToken?.Value<object>();
        }

        public static object GetValueWithSplit(this JObject jObject, string nodeName)
        {
            return GetValueWithSplit(jObject,nodeName,'.');
        }
    }
}
