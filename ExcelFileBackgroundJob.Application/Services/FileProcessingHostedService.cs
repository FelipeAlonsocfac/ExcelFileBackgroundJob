using ExcelFileBackgroundJob.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExcelFileBackgroundJob.Application.Services;

public class FileProcessingHostedService : BackgroundService
{
    private readonly FileProcessingService _fileProcessingService;
    private readonly IServiceProvider _serviceProvider;
    public FileProcessingHostedService(
        FileProcessingService fileProcessingService,
        IServiceProvider serviceProvider)
    {
        _fileProcessingService = fileProcessingService;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var fileName in _fileProcessingService.ReadAllAsync())
        {
            using var scope = _serviceProvider.CreateScope();

            var processor = scope.ServiceProvider.GetRequiredService<IFileService>();

            await processor.ProcessFileAsync(fileName);
        }
    }
}
