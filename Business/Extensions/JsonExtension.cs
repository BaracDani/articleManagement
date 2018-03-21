using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Business.Extensions
{
    public static class JsonExtension
    {
        public static string JsonSerialize(this object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public static T JsonDeserializeItem<T>(this string value)
        {
            T item;
            try
            {
                var serializer = new JavaScriptSerializer();
                item = serializer.Deserialize<T>(value);
            }
            catch (Exception)
            {
                item = default(T);
            }

            return item;
        }

        public static T[] JsonDeserialize<T>(this string value)
        {
            T[] list;
            try
            {
                var serializer = new JavaScriptSerializer();
                list = serializer.Deserialize<T[]>(value);
            }
            catch (Exception)
            {
                list = new T[0];
            }

            return list;
        }
    }
}
