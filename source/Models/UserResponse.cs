namespace Clientspace.Models;

/// <summary>
/// Response model for User API.
/// </summary>
public class UserGetResponse
{
    public List<UserResponse> Data { get; set; } = [];
}

public class UserResponse
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Company { get; set; }
    public string? UserName { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public bool IsExpired { get; set; }
}