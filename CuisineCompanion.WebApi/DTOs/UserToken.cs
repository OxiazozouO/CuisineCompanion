namespace CuisineCompanion.WebApi.DTOs;

public class UserToken
{
    public int UserId { get; set; }
    
    public string UserName { get; set; }
    
    public string Token { get; set; }
}