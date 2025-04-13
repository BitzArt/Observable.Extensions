using System;
using System.Threading.Tasks;

namespace BitzArt.Observable;

/// <summary>
/// Provides a mechanism for observing asynchronous events.
/// </summary>
/// <typeparam name="T">The type of the value being observed.</typeparam>
public interface IAsyncObserver<T> : IObserver<T>
{
    public Task OnNextAsync(T value);

    public Task OnCompletedAsync();

    public Task OnErrorAsync(Exception error);
}
