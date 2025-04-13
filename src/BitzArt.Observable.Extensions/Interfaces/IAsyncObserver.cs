using System;
using System.Threading;
using System.Threading.Tasks;

namespace BitzArt;

/// <summary>
/// Provides a mechanism for observing asynchronous events.
/// </summary>
/// <typeparam name="T">The type of the value being observed.</typeparam>
public interface IAsyncObserver<T>
{
    /// <inheritdoc cref="IObserver{T}.OnNext(T)"/>
    public Task OnNextAsync(T value, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IObserver{T}.OnCompleted"/>
    public Task OnCompletedAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IObserver{T}.OnError(Exception)"/>
    public Task OnErrorAsync(Exception error, CancellationToken cancellationToken = default);
}
