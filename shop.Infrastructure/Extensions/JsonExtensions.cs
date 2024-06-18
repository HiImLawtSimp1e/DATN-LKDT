using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace MicroBase.Share.Extensions
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializerSettings SerializerSetting = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string JsonSerialize<T>(this T entity, bool ignoreNullValues = false)
        {
            try
            {
                if (ignoreNullValues)
                {
                    SerializerSetting.NullValueHandling = NullValueHandling.Ignore;
                }

                return JsonConvert.SerializeObject(entity, SerializerSetting);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static T JsonDeserialize<T>(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(content, SerializerSetting);
            }
            catch (Exception ex)
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
        }
    }
}