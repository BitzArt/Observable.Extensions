using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BitzArt;

/// <summary>
/// A provider for async push-based events.
/// </summary>
/// <typeparam name="T">Type of observable value.</typeparam>
public sealed class AsyncObservable<T> : IAsyncObservable<T>, IDisposable
{
    private readonly List<IObserver<T>> _observers = [];

    private readonly Lock _lockObject = new();

    private bool _isDisposed = false;

    /// <summary>
    /// Subscribe an observer to this observable.
    /// </summary>
    /// <param name="observer">An observer to subscribe.</param>
    public IDisposable Subscribe(IObserver<T> observer)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        lock (_lockObject)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            if (_observers.Contains(observer))
                throw new InvalidOperationException("This observer is already subscribed to this observable.");

            _observers.Add(observer);

            return new Unsubscriber(_observers, observer, _lockObject);
        }
    }

    /// <summary>
    /// Provides all subscribed observers with new data.
    /// </summary>
    /// <param name="value">Value to notify observers with.</param>
    /// <param name="ignoreCancellation">Whether to ignore <see cref="TaskCanceledException"/> occurring in observer callbacks.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnNextAsync(T value, bool ignoreCancellation = true, CancellationToken cancellationToken = default)
        => await NotifyAsync(
            observer => observer.OnNext(value),
            observer => observer.OnNextAsync(value, cancellationToken),
            ignoreCancellation,
            cancellationToken);

    /// <summary>
    /// Notifies all subscribed observers that the provider has finished sending push-based notifications.
    /// </summary>
    /// <param name="ignoreCancellation">Whether to ignore <see cref="TaskCanceledException"/> occurring in observer callbacks.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnCompletedAsync(bool ignoreCancellation = true, CancellationToken cancellationToken = default)
    {
        await NotifyAsync(
            observer => observer.OnCompleted(),
            observer => observer.OnCompletedAsync(cancellationToken),
            ignoreCancellation,
            cancellationToken);

        Dispose();
    }

    /// <summary>
    /// Notifies all subscribed observers that the provider has encountered an error condition.
    /// </summary>
    /// <param name="error">An object that provides additional information about the error.</param>
    /// <param name="ignoreCancellation">Whether to ignore <see cref="TaskCanceledException"/> occurring in observer callbacks.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnErrorAsync(Exception error, bool ignoreCancellation = true, CancellationToken cancellationToken = default) => await NotifyAsync(
        observer => observer.OnError(error),
        observer => observer.OnErrorAsync(error, cancellationToken),
        ignoreCancellation,
        cancellationToken);

    private async Task NotifyAsync(Action<IObserver<T>> notify, Func<IAsyncObserver<T>, Task> notifyAsync, bool ignoreCancellation = true, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        var tasks = new List<Task>();

        foreach (var observer in _observers)
        {
            switch (observer)
            {
                case IAsyncObserver<T> asyncObserver:
                    tasks.Add(notifyAsync.Invoke(asyncObserver).IgnoreCancellation(ignoreCancellation));
                    break;
                default:
                    notify.Invoke(observer);
                    break;
            }
        }

        await Task.WhenAll(tasks);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        lock (_lockObject)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _observers.Clear();

            _isDisposed = true;

            GC.SuppressFinalize(this);
        }
    }

    private readonly struct Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer, Lock lockObject) : IDisposable
    {
        private readonly List<IObserver<T>> _observers = observers;

        private readonly IObserver<T> _observer = observer;

        private readonly Lock _lockObject = lockObject;

        public readonly void Dispose()
        {
            lock (_lockObject)
            {
                if (!_observers.Remove(_observer))
                    throw new ObjectDisposedException(nameof(Unsubscriber));
            }
        }
    }
}
