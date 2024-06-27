namespace CuisineCompanion.WebApi.Constant;

public static class IdCategoryConstant
{
    public const int Ingredient = 1;
    public const int Recipe = 2;
    public const int Menu = 3;

    public static string GetName(long id)
    {
        return id switch
        {
            Ingredient => "食材",
            Recipe => "食谱",
            Menu => "餐单",
            _ => ""
        };
    }
}