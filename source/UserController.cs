using Clientspace.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;

namespace Clientspace;

public class UserController
{
    private IConfiguration Configuration { get; set; }

    public UserController(IConfiguration configuration)
    {
        Configuration = configuration;
        if (Configuration["Clientspace:BaseURL"] == null) throw new Exception("Clientspace URL not found in configuration");
        if (Configuration["Clientspace:BearerToken"] == null) throw new Exception("Clientspace token not found in configuration");
    }
    
    public async Task<UserGetResponse?> GetActiveUsers(string? email = null)
    {
        UserGetResponse? result = new();

        var options = new RestClientOptions(Configuration["Clientspace:BaseURL"] ?? throw new InvalidOperationException())
        {
            Timeout = new TimeSpan(hours: 0, minutes: 5, seconds: 0)
        };
        var client = new RestClient(options);
        var request = new RestRequest("/api/user/v3.0/search");
        request.AddHeader("Authorization", $"Bearer {Configuration["Clientspace:BearerToken"] ?? throw new InvalidOperationException()}");

        if (email != null)
            request.AddQueryParameter("criteria.email", email);
        
        request.AddQueryParameter("criteria.status", "1");
        request.AddQueryParameter("criteria.pagingData.sortFieldName", "Name");

        RestResponse response = await client.ExecuteAsync(request);
        if (response is { IsSuccessful: true, Content: not null })
            result = JsonSerializer.Deserialize<UserGetResponse>(response.Content);
        else
            Console.WriteLine(response.Content);

        return result;
    }
}