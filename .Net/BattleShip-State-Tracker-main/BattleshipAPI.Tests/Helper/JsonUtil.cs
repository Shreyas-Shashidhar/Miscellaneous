using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Tests.Helper
{
    class JsonUtil
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            try
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(stringResponse);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
