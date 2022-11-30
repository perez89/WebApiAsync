namespace Domain;

public sealed class PrimeBgProcess : BackgroundService
{
    private readonly IChannel<Product> _productChannel;

    public PrimeBgProcess(IChannel<Product> productChannel)
    {
        _productChannel = productChannel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ProcessAsync(stoppingToken);
    }

    public async Task ProcessAsync(CancellationToken stoppingToken)
    {
        if (stoppingToken.IsCancellationRequested)
        {
            return;
        }

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var obj = await _productChannel.DequeueAsync(() => Task.CompletedTask, stoppingToken);

                if (obj is null)
                    continue;

                await ProcessPackageAsync(obj, stoppingToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR = " + ex.Message);

            //_logger.Error($"{nameof(PackagesAnalyzer)} - Error occurred while dequeuing an item", ex);
        }
    }

    private async Task ProcessPackageAsync(Product obj, CancellationToken cancellationToken)
    {
        await Task.Run(() => {
            Console.WriteLine("BG Service = " + JsonSerializer.Serialize(obj));
        }, cancellationToken); 
    }
}
