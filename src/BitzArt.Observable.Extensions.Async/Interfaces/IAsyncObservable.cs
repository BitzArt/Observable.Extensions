namespace BitzArt;

/// <summary>
/// Defines a provider for async push-based event notifications.
/// </summary>
/// <typeparam name="T">Type of observable value.</typeparam>
public interface IAsyncObservable<T> : IObservable<T>
{
}
