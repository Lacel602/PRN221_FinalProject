namespace PRN221_FinalProject.Session;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

public static class SessionExtensions
{
    public static void Set<T>(this ISession session, IHttpContextAccessor httpContextAccessor, string key, T value)
    {
        string serializedValue = JsonConvert.SerializeObject(value);
        httpContextAccessor.HttpContext.Session.SetString(key, serializedValue);
    }

    public static T Get<T>(this ISession session, IHttpContextAccessor httpContextAccessor, string key)
    {
        string serializedValue = httpContextAccessor.HttpContext.Session.GetString(key);
        if (serializedValue == null)
            return default;

        return JsonConvert.DeserializeObject<T>(serializedValue);
    }
}
