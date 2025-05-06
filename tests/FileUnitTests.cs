using Clientspace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Clientspace.Tests;

public class FileUnitTests
{
    private IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddUserSecrets<FileUnitTests>(true)
        .Build();

    [Fact]
    public async Task GetFile()
    {
        Guid fileId = Guid.Parse(configuration["TestData:fileId"] ?? Guid.Empty.ToString());

        FileController controller = new(configuration);
        var fileResponse = await controller.GetFile(fileId);

        if (fileResponse != null)
        {
            IActionResult response = fileResponse;
            Assert.NotNull(response);
        }

    }

    [Fact]
    public async Task AddFilesTest()
    {
        FileController controller = new(configuration);
        FileResponse? fileResponse = null;

        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "testfiles", "testing.docx")))
        {
            fileResponse = await controller.UploadFileToCase(
                Guid.Parse(configuration["TestData:testCaseId"] ?? Guid.Empty.ToString()),
                Path.Combine(Directory.GetCurrentDirectory(), "testfiles", "testing.docx"),
                "Test file upload (docx)"
            );

            if (fileResponse != null)
            {
                FileResponse response = fileResponse;
                Assert.NotNull(response);
            }
        }

        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "testfiles", "testing.pdf")))
        {
            fileResponse = await controller.UploadFileToCase(
                Guid.Parse(configuration["TestData:testCaseId"] ?? Guid.Empty.ToString()),
                Path.Combine(Directory.GetCurrentDirectory(), "testfiles", "testing.pdf"),
                "Test file upload (pdf)"
            );

            if (fileResponse != null)
            {
                FileResponse response = fileResponse;
                Assert.NotNull(response);
            }
        }

        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "testfiles", "testing.txt")))
        {
            fileResponse = await controller.UploadFileToCase(
                Guid.Parse(configuration["TestData:testCaseId"] ?? Guid.Empty.ToString()),
                Path.Combine(Directory.GetCurrentDirectory(), "testfiles", "testing.txt"),
                "Test file upload (txt)"
            );

            if (fileResponse != null)
            {
                FileResponse response = fileResponse;
                Assert.NotNull(response);
            }
        }
    }
}