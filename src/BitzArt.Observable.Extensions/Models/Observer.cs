using System;

namespace BitzArt;

/// <summary>
/// <inheritdoc cref="IObserver{T}"/>
/// </summary>
/// <typeparam name="T">
/// <inheritdoc cref="IObserver{T}"/>
/// </typeparam>/>
internal sealed class Observer<T> : IObserver<T>
{
    private readonly Action<T> _onNext;

    private readonly Action? _onCompleted;

    private readonly Action<Exception>? _onError;

    /// <summary>
    /// Initializes a new instance of the <see cref="Observer{T}"/> class.
    /// </summary>
    /// <param name="onNext">A callback that is invoked when the provider has new data.</param>
    /// <param name="onCompleted">A callback that is invoked when the provider has finished sending push-based notifications.</param>
    /// <param name="onError">A callback that is invoked when the provider has encountered an error condition.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public Observer(Action<T> onNext, Action? onCompleted = null, Action<Exception>? onError = null)
    {
        ArgumentNullException.ThrowIfNull(onNext);

        _onNext = onNext;
        _onCompleted = onCompleted;
        _onError = onError;
    }

    /// <summary>
    /// <inheritdoc cref="IObserver{T}.OnNext(T)"/>
    /// </summary>
    public void OnNext(T value) => _onNext(value);

    /// <summary>
    /// <inheritdoc cref="IObserver{T}.OnCompleted()"/>
    /// </summary>
    public void OnCompleted() => _onCompleted?.Invoke();

    /// <summary>
    /// <inheritdoc cref="IObserver{T}.OnError(Exception)"/>
    /// </summary>
    public void OnError(Exception error) => _onError?.Invoke(error);
}
