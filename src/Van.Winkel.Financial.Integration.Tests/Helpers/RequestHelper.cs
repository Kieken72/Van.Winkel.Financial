using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Van.Winkel.Financial.Integration.Tests.Helpers
{
    public static class RequestHelper
    {
        public static HttpContent CreateJson(object obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }
    }
}