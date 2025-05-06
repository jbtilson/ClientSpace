using Clientspace.Models;
using Microsoft.Extensions.Configuration;

namespace Clientspace.Tests;

public class CaseUnitTests
{
    private IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddUserSecrets<CaseUnitTests>(true)
        .Build();

    [Fact]
    public async Task GetCaseTest()
    {
        CaseController controller = new(configuration);
        var caseResponse = await controller.GetCase(Convert.ToInt32(configuration["TestData:testCaseId"]));
        if (caseResponse != null)
        {
            CaseGetResponse response = caseResponse;
            Assert.NotNull(response);
        }
    }

    [Fact]
    public async Task AddCaseTest()
    {
        // Assuming you have a valid workspaceId in your configuration
        CaseController controller = new(configuration);
        var caseResponse = await controller.AddCase(
            workspaceId: Convert.ToInt32(configuration["TestData:workspaceId"]),
            description: "Test Case",
            comment: "Test Comment",
            openedBy: "Test User",
            assignedTo: Convert.ToInt32(configuration["TestData:assignedTo"]),
            caseType: Convert.ToInt32(configuration["TestData:caseType"]),
            priority: "Medium",
            dueDate: DateTime.Now.AddDays(7));

        if (caseResponse != null && caseResponse.ID > 0)
        {
            CaseAddResponse response = caseResponse;
            Assert.NotNull(response);
        }
    }

}