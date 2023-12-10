/*
* John Moreau
* CSS233
* 12/10/2023
*/

using Newtonsoft.Json;

namespace CSS233_Final_SportsPro_JohnMoreau.Models
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var valueJson = session.GetString(key);
            if (string.IsNullOrEmpty(valueJson))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(valueJson);
        }
    }
}
