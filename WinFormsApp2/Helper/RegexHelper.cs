namespace WinFormsApp2.Helper;

/// <summary>
///     RegexUtil类的代码是来自[AndroidUtilCode](https://github.com/Blankj/AndroidUtilCode)的RegexUtils类和RegexConstants类
///     https://github.com/Blankj/AndroidUtilCode/blob/master/utilcode/src/main/java/com/blankj/utilcode/util/RegexUtils.java
///     https://github.com/Blankj/AndroidUtilCode/blob/master/utilcode/src/main/java/com/blankj/utilcode/constant/RegexConstants.java
/// </summary>
public static class RegexHelper
{
    /// <summary>
    ///     简单手机号的正则表达式。
    /// </summary>
    public const string MobileSimple = "^[1]\\d{10}$";

    /// <summary>
    ///     精确手机号的正则表达式。
    /// </summary>
    public const string MobileExact = "^((13[0-9])|(14[5,7])|(15[0-3,5-9])|(16[6])|(17[0,1,3,5-8])|" +
                                      "(18[0-9])|(19[8,9]))\\d{8}$";

    /// <summary>
    ///     电话号码的正则表达式。
    /// </summary>
    public const string Tel = "^0\\d{2,3}[- ]?\\d{7,8}";

    /// <summary>
    ///     15位身份证号的正则表达式。
    /// </summary>
    public const string IdCard15 = "^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$";

    /// <summary>
    ///     18位身份证号的正则表达式。
    /// </summary>
    public const string IdCard18 = "^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}" +
                                   "([0-9Xx])$";

    /// <summary>
    ///     电子邮件的正则表达式。
    /// </summary>
    public const string Email = "^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";

    /// <summary>
    ///     URL的正则表达式。
    /// </summary>
    public const string Url = "[a-zA-z]+://[^\\s]*";

    /// <summary>
    ///     汉字的正则表达式。
    /// </summary>
    public const string Zh = "^[\\u4e00-\\u9fa5]+$";

    /// <summary>
    ///     用户名的正则表达式。
    /// </summary>
    public const string Username = "^[\\w\\u4e00-\\u9fa5]{6,20}(?<!_)$";

    /// <summary>
    ///     日期的正则表达式，模式为"yyyy-MM-dd"。
    /// </summary>
    public const string Date = "^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|" +
                               "(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|" +
                               "(?:0[48]|[2468][048]|[13579][26])00)-02-29)$";

    /// <summary>
    ///     IP地址的正则表达式。
    /// </summary>
    public const string Ip = "((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)";

    ///////////////////////////////////////////////////////////////////////////
    // 以下来源于http://tool.oschina.net/regex
    ///////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///     双字节字符的正则表达式。
    /// </summary>
    public const string DoubleByteChar = "[^\\x00-\\xff]";

    /// <summary>
    ///     空白行的正则表达式。
    /// </summary>
    public const string BlankLine = "\\n\\s*\\r";

    /// <summary>
    ///     QQ号码的正则表达式。
    /// </summary>
    public const string QqNum = "[1-9][0-9]{4,}";

    /// <summary>
    ///     中国邮政编码的正则表达式。
    /// </summary>
    public const string ChinaPostalCode = "[1-9]\\d{5}(?!\\d)";

    /// <summary>
    ///     正整数的正则表达式。
    /// </summary>
    public const string PositiveInteger = "^[1-9]\\d*$";

    /// <summary>
    ///     负整数的正则表达式。
    /// </summary>
    public const string NegativeInteger = "^-[1-9]\\d*$";

    /// <summary>
    ///     整数的正则表达式。
    /// </summary>
    public const string Integer = "^-?[1-9]\\d*$";

    /// <summary>
    ///     非负整数的正则表达式。
    /// </summary>
    public const string NotNegativeInteger = "^[1-9]\\d*|0$";

    /// <summary>
    ///     非正整数的正则表达式。
    /// </summary>
    public const string NotPositiveInteger = "^-[1-9]\\d*|0$";

    /// <summary>
    ///     正浮点数的正则表达式。
    /// </summary>
    public const string PositiveFloat = "^[1-9]\\d*\\.\\d*|0\\.\\d*[1-9]\\d*$";

    /// <summary>
    ///     负浮点数的正则表达式。
    /// </summary>
    public const string NegativeFloat = "^-[1-9]\\d*\\.\\d*|-0\\.\\d*[1-9]\\d*$";

    ///////////////////////////////////////////////////////////////////////////
    // 如果需要更多，请访问 http://toutiao.com/i6231678548520731137
    ///////////////////////////////////////////////////////////////////////////
}