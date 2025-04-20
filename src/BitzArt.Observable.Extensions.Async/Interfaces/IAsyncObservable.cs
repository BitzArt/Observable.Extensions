namespace BitzArt;

/// <summary>
/// Defines a provider for asynchronous push-based notifications.
/// </summary>
/// <typeparam name="T">The type of object that provides notification information.</typeparam>
public interface IAsyncObservable<T> : IObservable<T>
{
}
