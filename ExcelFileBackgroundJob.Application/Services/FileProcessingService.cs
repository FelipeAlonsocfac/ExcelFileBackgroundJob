using System.Threading.Channels;

namespace ExcelFileBackgroundJob.Application.Services;

public class FileProcessingService
{
    private const int MaxMessagesInChannel = 100;
    private readonly Channel<string> _channel;

    public FileProcessingService()
    {
        var options = new BoundedChannelOptions(MaxMessagesInChannel)
        {
            SingleWriter = false,
            SingleReader = true
        };

        _channel = Channel.CreateBounded<string>(options);
    }

    public async Task<bool> AddFileAsync(string fileName, CancellationToken ct = default)
    {
        while (await _channel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
        {
            if (_channel.Writer.TryWrite(fileName))
            {
                return true;
            }
        }

        return false;
    }

    public IAsyncEnumerable<string> ReadAllAsync(CancellationToken ct = default) =>
        _channel.Reader.ReadAllAsync(ct);
}
