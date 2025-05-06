using Clientspace.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;

namespace Clientspace;

public class CaseController
{
    private string CaseTableName => "gen_ClientServiceCase";

    private IConfiguration Configuration { get; set; }

    public CaseController(IConfiguration configuration)
    {
        Configuration = configuration;
        if (Configuration["Clientspace:BaseURL"] == null) throw new Exception("Clientspace URL not found in configuration");
        if (Configuration["Clientspace:BearerToken"] == null) throw new Exception("Clientspace token not found in configuration");
    }

    public async Task<CaseGetResponse?> GetCase(int id)
    {
        CaseGetResponse? result = new();

        var options = new RestClientOptions(Configuration["Clientspace:BaseURL"] ?? throw new InvalidOperationException())
        {
            Timeout = new TimeSpan(hours: 0, minutes: 5, seconds: 0)
        };
        var client = new RestClient(options);
        var request = new RestRequest($"/api/dataform/v3.0/{CaseTableName}/{id}");
        request.AddHeader("Authorization", $"Bearer {Configuration["Clientspace:BearerToken"] ?? throw new InvalidOperationException()}");

        RestResponse response = await client.ExecuteAsync(request);
        if (response is { IsSuccessful: true, Content: not null })
            result = JsonSerializer.Deserialize<CaseGetResponse>(response.Content);
        else
            Console.WriteLine(response.Content);

        return result;
    }

    public async Task<CaseAddResponse?> AddCase(int workspaceId, string description, string comment, string openedBy, int assignedTo, int caseType, string priority = "Medium", DateTime? dueDate = null)
    {
        CaseAddResponse? result = new();

        var options = new RestClientOptions(Configuration["Clientspace:BaseURL"] ?? throw new InvalidOperationException())
        {
            Timeout = new TimeSpan(hours: 0, minutes: 5, seconds: 0)
        };
        var client = new RestClient(options);
        var request = new RestRequest($"/api/dataform/v3.0/add3.0/{CaseTableName}/{workspaceId}", method: Method.Post);
        request.AddHeader("Authorization", $"Bearer {Configuration["Clientspace:BearerToken"] ?? throw new InvalidOperationException()}");

        // Create the case object
        var body = @"{
            " + "\n" +
            @" ""Values"": {
            " + "\n" +
            @"    ""Archive"": false,
            " + "\n" +
            $@"    ""AssignedToChangeDate"": ""{DateTime.Now:yyyy-MM-ddTHH:mm:ssZ}"",
            " + "\n" +
            $@"    ""BrokerContact"": {assignedTo},
            " + "\n" +
            $@"    ""CallerName"": ""{openedBy}"",
            " + "\n" +
            @"    ""CaseAudit"": null,
            " + "\n" +
            @"    ""CaseNumber"": null,
            " + "\n" +
            $@"    ""CaseInfo"": ""{comment}"",
            " + "\n" +
            @"    ""CaseNotes"": """",
            " + "\n" +
            @"    ""CommunicationMethod"": """",
            " + "\n" +
            @"    ""crCategory"": ""IT"",
            " + "\n" +
            $@"    ""CreateDate"": ""{DateTime.Now:yyyy-MM-ddTHH:mm:ssZ}"",
            " + "\n" +
            $@"    ""CreateTime"": ""{DateTime.Now:yyyy-MM-ddTHH:mm:ssZ}"",
            " + "\n" +
            $@"    ""DueDate"": ""{dueDate:yyyy-MM-ddTHH:mm:ssZ}"",
            " + "\n" +
            @"    ""EmailAddress"": """",
            " + "\n" +
            @"    ""Escalation2Date"": null,
            " + "\n" +
            @"    ""Escalation2Time"": null,
            " + "\n" +
            @"    ""EscalationDate"": null,
            " + "\n" +
            @"    ""EscalationTime"": null,
            " + "\n" +
            @"    ""ExternalReportedByID"": """",
            " + "\n" +
            @"    ""FirstResponseDate"": null,
            " + "\n" +
            $@"    ""fkCaseTypeID"": {caseType},
            " + "\n" +
            @"    ""fkContactIDClientContact"": null,
            " + "\n" +
            @"    ""fkEmployeeID"": null,
            " + "\n" +
            @"    ""fkOwnerUserID"": null,
            " + "\n" +
            @"    ""fkReportedByEmployeeID"": null,
            " + "\n" +
            $@"    ""fkUserIDAssignedTo"": {assignedTo},
            " + "\n" +
            $@"    ""fkUserIDCreatedBy"": {assignedTo},
            " + "\n" +
            @"    ""hasNotificationsDisabled"": false,
            " + "\n" +
            @"    ""HoursToComplete"": 0.00,
            " + "\n" +
            @"    ""IncludeinNotification"": true,
            " + "\n" +
            @"    ""InternalNotes"": """",
            " + "\n" +
            $@"    ""luPriority"": ""{priority}"",
            " + "\n" +
            @"    ""luStatus"": ""New"",
            " + "\n" +
            @"    ""PhoneNumber"": """",
            " + "\n" +
            @"    ""Resolution"": """",
            " + "\n" +
            @"    ""ResolutionDate"": null,
            " + "\n" +
            @"    ""ResolutionTime"": null,
            " + "\n" +
            $@"    ""StatusChangeDate"": ""{DateTime.Now:yyyy-MM-ddTHH:mm:ssZ}"",
            " + "\n" +
            $@"    ""Subject"": ""{description}"",
            " + "\n" +
            @"    ""TimeSpent"": null,
            " + "\n" +
            @"    ""UpdatedFieldsExternal"": ""Status"",
            " + "\n" +
            @"    ""UpdatedFieldsInternal"": ""Status""
            " + "\n" +
            @" }
            " + "\n" +
            @"}";
            

        // Add the case to the request
        request.AddStringBody(body, DataFormat.Json);

        RestResponse response = await client.ExecuteAsync(request);
        if (response is { IsSuccessful: true, Content: not null })
            result = JsonSerializer.Deserialize<CaseAddResponse>(response.Content);
        else
            Console.WriteLine(response.Content);

        return result;
    }

}