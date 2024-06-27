using System;

namespace CuisineCompanion.Helper;

public static class DataTimeHelper
{
    public static int GetAge(this DateTime birthDate)
    {
        return (int)((DateTime.Now - birthDate).TotalDays / 365.2425);
    }
}