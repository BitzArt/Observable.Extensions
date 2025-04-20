namespace BitzArt;

/// <summary>
/// Extension methods for <see cref="IObservable{T}"/>.
/// </summary>
public static class ObservableExtensions
{
    /// <inheritdoc cref="IObservable{T}.Subscribe(IObserver{T})"/>
    public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext, Action? onCompleted = null, Action<Exception>? onError = null)
    {
        ArgumentNullException.ThrowIfNull(observable);

        var observer = new Observer<T>(onNext, onCompleted, onError);
        var unsubscriber = observable.Subscribe(observer);

        return unsubscriber;
    }
}
