using Microsoft.AspNetCore.Http;

namespace ExcelFileBackgroundJob.Core.Dto;

public class FileUploadModel
{
    public IFormFile FileDetails { get; set; }
    public string FileType { get; set; }
}
