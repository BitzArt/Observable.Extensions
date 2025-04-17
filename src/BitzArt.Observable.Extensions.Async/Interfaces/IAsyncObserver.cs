namespace BitzArt;

/// <summary>
/// Provides a mechanism for receiving asynchronous push-based notifications.
/// </summary>
/// <typeparam name="T">The type of object that provides notification information.</typeparam>
public interface IAsyncObserver<T>
{
    /// <inheritdoc cref="IObserver{T}.OnNext(T)"/>
    public Task OnNextAsync(T value, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IObserver{T}.OnCompleted"/>
    public Task OnCompletedAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IObserver{T}.OnError(Exception)"/>
    public Task OnErrorAsync(Exception error, CancellationToken cancellationToken = default);
}
