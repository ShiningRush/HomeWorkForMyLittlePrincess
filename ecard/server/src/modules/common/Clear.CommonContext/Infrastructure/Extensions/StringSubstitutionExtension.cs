using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Extensions;

namespace Clear.CommonContext.Infrastructure.Extensions
{
    public static class StringSubstitutionExtension
    {
        private static readonly Regex Pattern = new Regex(@"(?<!\{)\{(\w+)([^\}]*)\}");

        private static readonly Regex JsonPattern = new Regex(@"\{([^\{^\}]*)\}");

        /// <summary>
        /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified dictionary.
        /// </summary>
        public static String Subtitute(this String template, IDictionary<String, Object> dictionary)
        {
            return Subtitute(template, null, dictionary);
        }

        /// <summary>
        /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified dictionary.
        /// A specified parameter supplies culture-specific formatting information.
        /// </summary>
        public static String Subtitute(this String template, IFormatProvider formatProvider, IDictionary<String, Object> dictionary)
        {
            if (template.IsNullOrWhiteSpace()) return null;

            var map = new Dictionary<String, int>();

            var list = new List<Object>();

            var format = Pattern.Replace(
                template,
                match =>
                {
                    var name = match.Groups[1].Captures[0].Value;

                    if (!map.ContainsKey(name))
                    {
                        map[name] = map.Count;
                        list.Add(dictionary.ContainsKey(name) ? dictionary[name] : null);
                    }

                    return "{" + map[name] + match.Groups[2].Captures[0].Value + "}";
                }
                );

            return formatProvider == null
                ?
                String.Format(format, list.ToArray())
                :
                String.Format(formatProvider, format, list.ToArray());
        }


        public static String Subtitute(this String template, string jsonString)
        {
            return Subtitute(template, null, jsonString);
        }

        public static String Subtitute(this String template, IFormatProvider formatProvider, string jsonString)
        {
            if (template.IsNullOrWhiteSpace()) return null;
            var map = new Dictionary<String, int>();

            var list = new List<Object>();

            var jObject = JObject.Parse(jsonString);

            var format = JsonPattern.Replace(
                template,
                match =>
                {
                    var name = match.Groups[1].Captures[0].Value;

                    if (!map.ContainsKey(name))
                    {
                        map[name] = map.Count;
                        list.Add(jObject.GetValueWithSplit(name));
                    }

                    return "{" + map[name] +"}";
                }
                );
            try
            {
                return formatProvider == null
               ?
               String.Format(format, list.ToArray())
               :
               String.Format(formatProvider, format, list.ToArray());
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        public static String Subtitute(this String template, Object arg)
        {
            return Subtitute(template,null, arg);
        }

        public static String Subtitute(this String template, IFormatProvider formatProvider, Object arg)
        {
            var map = new Dictionary<String, int>();

            var list = new List<Object>();

            var format = Pattern.Replace(
                template,
                match =>
                {
                    var name = match.Groups[1].Captures[0].Value;

                    if (!map.ContainsKey(name))
                    {
                        map[name] = map.Count;
                        var value = arg.GetType().GetProperty(name)?.GetValue(arg);
                        list.Add(value);
                    }

                    return "{" + map[name] + match.Groups[2].Captures[0].Value + "}";
                }
                );

            return formatProvider == null
                ?
                String.Format(format, list.ToArray())
                :
                String.Format(formatProvider, format, list.ToArray());
        }
    }
}
