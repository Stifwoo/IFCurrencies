using System.Web.Script.Serialization;

namespace IFCurrenciesApi.Helper
{
    public static class JsonSerializator<T>
    {
        public static T Deserialize(string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
    }
}