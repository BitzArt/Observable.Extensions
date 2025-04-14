using System;
using System.Threading;
using System.Threading.Tasks;

namespace BitzArt;

/// <summary>
/// <inheritdoc cref="IAsyncObserver{T}"/>
/// </summary>
/// <typeparam name="T">
/// <inheritdoc cref="IAsyncObserver{T}"/>
/// </typeparam>/>
internal sealed class AsyncObserver<T> : IAsyncObserver<T>, IObserver<T>
{
    private readonly Func<T, CancellationToken, Task> _onNext;

    private readonly Func<CancellationToken, Task>? _onCompleted;

    private readonly Func<Exception, CancellationToken, Task>? _onError;

    /// <inheritdoc cref="AsyncObserver{T}.AsyncObserver(Func{T, CancellationToken, Task}, Func{CancellationToken, Task}?, Func{Exception, CancellationToken, Task}?)"/>
    public AsyncObserver(
        Func<T, Task> onNext,
        Func<Task>? onCompleted = null,
        Func<Exception, Task>? onError = null) : this(
            (T value, CancellationToken _) => onNext(value),
            onCompleted is not null
            ? (CancellationToken _) => onCompleted()
            : null,
            onError is not null
            ? (Exception ex, CancellationToken _) => onError(ex)
            : null)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncObserver{T}"/> class.
    /// </summary>
    /// <param name="onNext">A callback that is invoked when the provider has new data.</param>
    /// <param name="onCompleted">A callback that is invoked when the provider has finished sending push-based notifications.</param>
    /// <param name="onError">A callback that is invoked when the provider has encountered an error condition.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public AsyncObserver(
        Func<T, CancellationToken, Task> onNext,
        Func<CancellationToken, Task>? onCompleted = null,
        Func<Exception, CancellationToken, Task>? onError = null)
    {
        ArgumentNullException.ThrowIfNull(onNext);

        _onNext = onNext;
        _onCompleted = onCompleted;
        _onError = onError;
    }

    /// <summary>
    /// <inheritdoc cref="IAsyncObserver{T}.OnNextAsync(T, CancellationToken)"/>
    /// </summary>
    public Task OnNextAsync(T value, CancellationToken cancellationToken = default)
        => _onNext.Invoke(value, cancellationToken);

    /// <summary>
    /// <inheritdoc cref="IAsyncObserver{T}.OnCompletedAsync(CancellationToken)"/>
    /// </summary>
    public Task OnCompletedAsync(CancellationToken cancellationToken = default)
        => _onCompleted?.Invoke(cancellationToken) ?? Task.CompletedTask;

    /// <summary>
    /// <inheritdoc cref="IAsyncObserver{T}.OnErrorAsync(Exception, CancellationToken)"/>
    /// </summary>
    public Task OnErrorAsync(Exception error, CancellationToken cancellationToken = default)
        => _onError?.Invoke(error, cancellationToken) ?? Task.CompletedTask;

    private const string SyncMethodNotSupportedMessage
        = "Synchronous IObserver methods are not supported by this observer implementation. Use ones provided by IAsyncObserver instead.";

    /// <inheritdoc/>
    public void OnNext(T value) => throw new NotSupportedException(SyncMethodNotSupportedMessage);

    /// <inheritdoc/>
    public void OnCompleted() => throw new NotSupportedException(SyncMethodNotSupportedMessage);

    /// <inheritdoc/>
    public void OnError(Exception error) => throw new NotSupportedException(SyncMethodNotSupportedMessage);
}
