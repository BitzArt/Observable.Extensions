using System;
using System.Threading.Tasks;

namespace BitzArt.Observable;

/// <summary>
/// <inheritdoc cref="IAsyncObserver{T}"/>
/// </summary>
/// <typeparam name="T">
/// <inheritdoc cref="IAsyncObserver{T}"/>
/// </typeparam>/>
public class AsyncObserver<T> : IAsyncObserver<T>, IDisposable
{
    private readonly IObservable<T> _observable;
    private readonly IDisposable _subscription;

    private readonly Func<T?, Task> _onNext;

    private readonly Func<Task>? _onCompleted;

    private readonly Func<Exception, Task>? _onError;

    private bool _isDisposed = false;

    /// <summary>
    /// The current value of the observable.
    /// </summary>
    public T? CurrentValue { get; private set; } = default;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncObserver{T}"/> class.
    /// </summary>
    /// <param name="observable"></param>
    /// <param name="onNext"></param>
    /// <param name="onCompleted"></param>
    /// <param name="onError"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public AsyncObserver(
        IObservable<T> observable,
        Func<T?, Task> onNext,
        Func<Task>? onCompleted = null,
        Func<Exception, Task>? onError = null)
    {
        ArgumentNullException.ThrowIfNull(observable, nameof(observable));
        _observable = observable;

        ArgumentNullException.ThrowIfNull(onNext, nameof(onNext));
        _onNext = onNext;

        _onCompleted = onCompleted;
        _onError = onError;

        _subscription = _observable.Subscribe(this);
    }

    /// <summary>
    /// <inheritdoc cref="IAsyncObserver{T}.OnNextAsync(T)"/>
    /// </summary>
    public Task OnNextAsync(T? value)
    {
        CurrentValue = value;

        _onNext(value);

        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc cref="IAsyncObserver{T}.OnCompletedAsync()"/>
    /// </summary>
    public Task OnCompletedAsync()
        => _onCompleted?.Invoke() ?? Task.CompletedTask;

    /// <summary>
    /// <inheritdoc cref="IAsyncObserver{T}.OnErrorAsync(Exception)"/>
    /// </summary>
    public Task OnErrorAsync(Exception error)
        => _onError?.Invoke(error) ?? Task.CompletedTask;

    public void OnNext(T? value) => OnNextAsync(value).Wait();

    public void OnCompleted() => OnCompletedAsync().Wait();

    public void OnError(Exception error) => OnErrorAsync(error).Wait();

    /// <summary>
    /// Disposes this <see cref="AsyncObserver{T}"/> and unsubscribes it from the observable.
    /// </summary>
    public void Dispose()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, nameof(AsyncObserver<T>));

        _subscription.Dispose();
        _isDisposed = true;
    }
}
