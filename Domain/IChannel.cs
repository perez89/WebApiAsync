namespace Domain;

public interface IChannel<T>
{
    Task AddAsync(T obj, CancellationToken cancellationToken);
    Task<T> DequeueAsync(Func<Task> validator, CancellationToken cancellationToken);
}