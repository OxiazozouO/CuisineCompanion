using System;
using System.Text;

namespace CuisineCompanion.Helper;

public static class StringHelper
{
    public static string GetRandomString()
    {
        return new StringBuilder()
            .Append(DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"))
            .Append(Guid.NewGuid().ToString().Replace("-", ""))
            .ToString();
    }

    /// <summary> 手机号码脱敏 </summary>
    public static string PhoneDesensitization(string telephone)
    {
        return telephone[..3] + "****   ****";
    }
}