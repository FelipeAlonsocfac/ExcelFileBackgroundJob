using ExcelFileBackgroundJob.Application.Services;
using ExcelFileBackgroundJob.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace ExcelFileBackgroundJob.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly FileProcessingService _fileProcessingService;
    private readonly ILogger<FileController> _logger;
    private string _filepath;

    public FileController(FileProcessingService fileProcessingService, ILogger<FileController> logger)
    {
        _fileProcessingService = fileProcessingService;
        _logger = logger;
    }

    /// <summary>
    /// Processes excel file.
    /// </summary>
    /// <param name="fileDetails"></param>
    /// <returns></returns>
    [HttpPost("process-file")]
    public async Task<ActionResult> UploadExcel([FromForm] FileUploadModel fileDetails, CancellationToken cancellationToken)
    {
        try
        {
            if (!await VerifyFileAndSave(fileDetails)) return BadRequest();

            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(3)); // wait max 3 seconds
            var fileWritten = await _fileProcessingService.AddFileAsync(_filepath, cts.Token);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (fileWritten)
            {
                //send notification: file could not be processed
            }

            return Ok();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<bool> VerifyFileAndSave(FileUploadModel fileDetails)
    {
        if (fileDetails == null || !fileDetails.FileType.Contains("xls") || !(fileDetails.FileDetails.Length > 0))
        {
            return false;
        }
        this._filepath = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileDetails.FileDetails.FileName);
        using (var fileStream = new FileStream(_filepath, FileMode.Create))
        {
            await fileDetails.FileDetails.CopyToAsync(fileStream);
        }

        return true;
    }
}
