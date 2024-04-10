using Newtonsoft.Json;
using System;

namespace utils
{
    public class JsonUtil
    {
        public static String toJson(Object jso)
        {
            return JsonConvert.SerializeObject(jso);
        }

        public static T fromJson<T>(String jso)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jso);
            }
            catch (Exception)
            {

            }
            return default;
        }
    }
}
