namespace Clientspace.Models;

public class FileResponse
{

    public Guid? LinkGuid { get; set; }
    public Guid? EntityRowGUID { get; set; }
    public string? TableName { get; set; }
    public string? FieldName { get; set; }
    public List<File>? Files { get; set; }
    public List<FileAddResponseError>? Errors { get; set; }

    public class File
    {
        public string? FileName { get; set; }
        public Guid? FileGUID { get; set; }
        public int Version { get; set; }
        public long ContentLength { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsPinned { get; set; }
    }    

    public class FileAddResponseError
    {
        public string? Message { get; set; }
        public int Code { get; set; }
        public bool CanSaveAnyway { get; set; }

        // Example of the response from ClientSpace API
        //   "Message": "string",
        //   "Code": 0,
        //   "CanSaveAnyway": true
    }
}
