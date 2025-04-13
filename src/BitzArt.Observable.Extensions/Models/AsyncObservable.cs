using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitzArt.Observable;

/// <summary>
/// A provider for async push-based events.
/// </summary>
/// <typeparam name="T">Type of observable value.</typeparam>
public struct AsyncObservable<T> : IObservable<T>
{
    private List<IObserver<T>> _observers = [];

    private readonly object _lockObject = new();

    /// <summary>
    /// Initializes a new <see cref="AsyncObservable{T}"/>.
    /// </summary>
    public AsyncObservable() 
    {
    }

    /// <summary>
    /// Subscribe an observer to this observable.
    /// </summary>
    /// <param name="observer">An observer to subscribe.</param>
    public IDisposable Subscribe(IObserver<T> observer)
    {
        lock (_lockObject)
        {
            if (_observers.Contains(observer))
                throw new InvalidOperationException("Observer already subscribed.");

            _observers.Add(observer);

            return new Unsubscriber(_observers, observer, _lockObject);
        }
    }

    /// <summary>
    /// Notify all subscribed observers with the specified value.
    /// </summary>
    /// <param name="value">A value to notify observers with.</param>
    public async Task NotifyObserversAsync(T value)
    {
        foreach (var observer in _observers)
        {
            if (observer is IAsyncObserver<T> asyncObserver)
            {
                await asyncObserver.OnNextAsync(value);
                continue;
            }

            observer.OnNext(value);
        }
    }

    private struct Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer, object lockObject) : IDisposable
    {
        private readonly List<IObserver<T>> _observers = observers;

        private readonly IObserver<T> _observer = observer;

        private readonly object _lockObject = lockObject;

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
