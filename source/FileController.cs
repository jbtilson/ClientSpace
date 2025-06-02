using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.StaticFiles;
using Clientspace.Models;
using RestSharp;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Clientspace;

public class FileController
{
    private IConfiguration Configuration { get; set; }
    
    public FileController(IConfiguration configuration)
    {
        Configuration = configuration;
        if (Configuration["Clientspace:BaseURL"] == null) throw new Exception("Clientspace URL not found in configuration");
        if (Configuration["Clientspace:BearerToken"] == null) throw new Exception("Clientspace token not found in configuration");
    }

    public async Task<IActionResult?> GetFile(Guid fileId)
    {
        var options = new RestClientOptions(Configuration["Clientspace:BaseURL"] ?? throw new InvalidOperationException())
        {
            Timeout = new TimeSpan(hours: 0, minutes: 5, seconds: 0)
        };
        var client = new RestClient(options);
        var request = new RestRequest($"/api/file/v3.0/download/{fileId}");
        request.AddHeader("Authorization", $"Bearer {Configuration["Clientspace:BearerToken"] ?? throw new InvalidOperationException()}");

        RestResponse response = await client.ExecuteAsync(request);
        if (response is { IsSuccessful: true, Content: not null })
        {
            byte?[] pdfBytes = [.. response.Content.Select(a => (byte?)a)];
            if (pdfBytes == null || pdfBytes.Length == 0)
                return new NotFoundResult();
            byte[] nonNullablePdfBytes = [.. pdfBytes.Where(b => b.HasValue).Select(b => b!.Value)];
            MemoryStream ms = new MemoryStream(nonNullablePdfBytes);
            return new FileStreamResult(ms, response.ContentType);
        }
        else
            Console.WriteLine(response.Content);
        
        return new NotFoundResult();
    }

    public async Task<FileResponse?> UploadFileToCase(Guid caseId, string fullFilePath, string? description = null)
    {
        FileResponse? result = new();

        // Check if the file was provided
        if (string.IsNullOrEmpty(fullFilePath))
            throw new ArgumentException("File path cannot be null or empty", nameof(fullFilePath));

        // Check if the file exists
        if (!File.Exists(fullFilePath))
            throw new FileNotFoundException("File not found", fullFilePath);

        // Determine the file specifics
        string base64File = EncodeFileToBase64(fullFilePath);
        double fileSize = GetFileSizeFromBase64String(base64File);
        string mimeType = GetMimeTypeForFileExtension(fullFilePath);
        string fileName = Path.GetFileName(fullFilePath);


        // Create the request
        var options = new RestClientOptions(Configuration["Clientspace:BaseURL"] ?? throw new InvalidOperationException())
        {
            Timeout = new TimeSpan(hours: 0, minutes: 5, seconds: 0)
        };
        var client = new RestClient(options);
        var request = new RestRequest($"/api/file/v3.0/add", method: Method.Post);
        request.AddHeader("Authorization", $"Bearer {Configuration["Clientspace:BearerToken"] ?? throw new InvalidOperationException()}");

        var body = @"{" + "\n" +
            @$"  ""FileName"": ""{fileName}""," + "\n" +
            @$"  ""EntityRowGUID"": ""{caseId}""," + "\n" +
            @"  ""TableName"": ""gen_ClientServiceCase""," + "\n" +
            @$"  ""ContentLength"": {fileSize}," + "\n" +
            @$"  ""ContentType"": ""{mimeType}""," + "\n" +
            @$"  ""Description"": ""{description}""," + "\n" +
            @"  ""MetaTags"": []," + "\n" +
            @$"  ""Content"": ""{base64File}""" + "\n" +
            @"}";

        // Add the case to the request
        request.AddStringBody(body, DataFormat.Json);

        RestResponse response = await client.ExecuteAsync(request);
        if (response is { IsSuccessful: true, Content: not null })
            result = JsonSerializer.Deserialize<FileResponse>(response.Content);
        else
            Console.WriteLine(response.Content);

        return result;
    }

    private string EncodeFileToBase64(string filePath)
    {
        // Read the file content and convert it to Base64
        byte[] fileBytes = File.ReadAllBytes(filePath);
        return Convert.ToBase64String(fileBytes);
    }
    private double GetFileSizeFromBase64String(string base64String)
    {
        if (string.IsNullOrEmpty(base64String)) return 0;
        var base64Length = base64String.AsSpan()[(base64String.IndexOf(',') + 1)..].Length;
        var fileSizeInByte = Math.Ceiling((double)base64Length / 4) * 3;
        return fileSizeInByte > 0 ? fileSizeInByte / 1 : 0;
    }    

    private string GetMimeTypeForFileExtension(string filePath)
    {
        const string DefaultContentType = "application/octet-stream";

        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(filePath, out string contentType))
        {
            contentType = DefaultContentType;
        }

        return contentType;
    }
}
