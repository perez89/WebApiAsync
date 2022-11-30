namespace Domain;

public sealed class QueueChannel<T> : IChannel<T> where T : Product
{
    private const int MAX_BOUND_CHANNEL = 10;
    private readonly ChannelWriter<T> _writer;
    private readonly ChannelReader<T> _reader;

    public QueueChannel()
    {
        var options = new BoundedChannelOptions(MAX_BOUND_CHANNEL)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        var myChannel = Channel.CreateBounded<T>(options);

        _writer = myChannel.Writer;
        _reader = myChannel.Reader;
    }

    public async Task AddAsync(T obj, CancellationToken cancellationToken)
    {
        await _writer.WaitToWriteAsync(cancellationToken);
        await _writer.WriteAsync(obj, cancellationToken);
    }

    public async Task<T> DequeueAsync(Func<Task> validator, CancellationToken cancellation)
    {
        await validator();

        var obj = await _reader.ReadAsync(cancellation);
        return obj;
    }
}
