using System.IO;
using Newtonsoft.Json;

namespace CuisineCompanion.Helper;

public static class JsonHelper
{
    public static string ToJson(this object data)
    {
        return JsonConvert.SerializeObject(data);
    }

    public static T ToEntity<T>(this object? json)
    {
        return JsonConvert.DeserializeObject<T>(json.ToString()) ??
               throw new FileLoadException("读取json失败！");
    }
}