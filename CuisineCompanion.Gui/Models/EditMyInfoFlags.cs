using System;

namespace CuisineCompanion.Models;

[Flags]
public enum EditMyInfoFlags
{
    Info = 1 << 0,
    Password = 1 << 1,
    Logout = 1 << 2
}