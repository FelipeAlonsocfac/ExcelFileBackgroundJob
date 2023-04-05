namespace ExcelFileBackgroundJob.Application.Interfaces;

public interface IFileService
{
    Task ProcessFileAsync(string saveToPath);
}
