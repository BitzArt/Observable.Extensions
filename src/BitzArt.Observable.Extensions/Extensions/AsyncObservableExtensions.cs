using System;
using System.Threading;
using System.Threading.Tasks;

namespace BitzArt;

/// <summary>
/// Extension methods for <see cref="IObservable{T}"/>.
/// </summary>
public static class ObservableExtensions
{
    /// <inheritdoc cref="IObservable{T}.Subscribe(IObserver{T})"/>
    public static IDisposable Subscribe<T>(this IObservable<T> asyncObservable, Action<T> onNext, Action? onCompleted = null, Action<Exception>? onError = null)
    {
        ArgumentNullException.ThrowIfNull(asyncObservable);

        var asyncObserver = new Observer<T>(onNext, onCompleted, onError);
        var unsubscriber = asyncObservable.Subscribe(asyncObserver);

        return unsubscriber;
    }
}

/// <summary>
/// Extension methods for <see cref="IAsyncObservable{T}"/>.
/// </summary>
public static class AsyncObservableExtensions
{
    /// <inheritdoc cref="IObservable{T}.Subscribe(IObserver{T})"/>
    public static IDisposable Subscribe<T>(this IAsyncObservable<T> asyncObservable, Func<T, Task> onNext, Func<Task>? onCompleted = null, Func<Exception, Task>? onError = null)
    {
        ArgumentNullException.ThrowIfNull(asyncObservable);

        var asyncObserver = new AsyncObserver<T>(onNext, onCompleted, onError);
        var unsubscriber = asyncObservable.Subscribe(asyncObserver);

        return unsubscriber;
    }

    /// <inheritdoc cref="IObservable{T}.Subscribe(IObserver{T})"/>
    public static IDisposable Subscribe<T>(this IAsyncObservable<T> asyncObservable, Func<T, CancellationToken, Task> onNext, Func<CancellationToken, Task>? onCompleted = null, Func<Exception, CancellationToken, Task>? onError = null)
    {
        ArgumentNullException.ThrowIfNull(asyncObservable);

        var asyncObserver = new AsyncObserver<T>(onNext, onCompleted, onError);
        var unsubscriber = asyncObservable.Subscribe(asyncObserver);

        return unsubscriber;
    }
}
