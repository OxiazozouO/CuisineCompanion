namespace CuisineCompanion.Extensions;

public static class RegexHelper
{
    /// <summary> 精确手机号的正则表达式。 </summary>
    public const string MobileExact = "^((13[0-9])|(14[5,7])|(15[0-3,5-9])|(16[6])|(17[0,1,3,5-8])|" +
                                      "(18[0-9])|(19[8,9]))\\d{8}$";

    /// <summary> 电子邮件的正则表达式。 </summary>
    public const string Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
}