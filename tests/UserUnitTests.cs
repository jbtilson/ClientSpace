using Clientspace.Models;
using Microsoft.Extensions.Configuration;

namespace Clientspace.Tests;

public class UserUnitTests
{
    private IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddUserSecrets<CaseUnitTests>(true)
        .Build();

    [Fact]
    public async Task GetActiveUsersTest()
    {
        UserController controller = new(configuration);
        var userResponse = await controller.GetActiveUsers(email: configuration["TestData:usersEmailFilter"]);
        if (userResponse != null)
        {
            UserGetResponse response = userResponse;
            Assert.NotNull(response);
            Assert.True(response.Data.Count > 0);
        }
    }
   
}