namespace Domain;

public class ProductManager : IProductManager
{
    private readonly IChannel<Product> _channel;

    public ProductManager(IChannel<Product> channel)
    {
        _channel = channel;
    }

    public async Task Add(Product product, CancellationToken cancellationToken) {
        await _channel.AddAsync(product, cancellationToken);
    }
}
