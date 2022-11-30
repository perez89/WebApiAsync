namespace Domain;

public interface IProductManager
{
    Task Add(Product product, CancellationToken cancellationToken);
}