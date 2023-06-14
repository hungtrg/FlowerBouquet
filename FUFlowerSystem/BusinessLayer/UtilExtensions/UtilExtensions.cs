using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace BusinessLayer.UtilExtensions
{
    public static class UtilExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object obj)
        {
            string value = JsonConvert.SerializeObject(obj);
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            session.Set(key, bytes);
        }

        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            string value;
            if (session.TryGetValue(key, out byte[] bytes))
            {
                value = Encoding.UTF8.GetString(bytes);
            }
            else
            {
                value = null;
            }
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
