namespace BitzArt.Observable.Extensions.Async.Tests;

public class AsyncObserverTests
{
    [Fact]
    public async Task OnNextAsync_WhenObserverInitializedWithOnNextCallback_ShouldTriggerAndAwaitCallback()
    {
        // Arrange
        var triggered = false;
        AsyncObserver<bool?> observer = new(
            onNext: async (_) =>
            {
                await Task.Delay(100);
                triggered = true;
            });

        // Act / Assert
        var task = observer.OnNextAsync(null!);
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnCompletedAsync_WhenObserverInitializedWithOnCompletedCallback_ShouldTriggerAndAwaitCallback()
    {
        // Arrange
        var triggered = false;
        AsyncObserver<bool> observer = new(
            onCompleted: async () =>
            {
                await Task.Delay(100);
                triggered = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Act / Assert
        var task = observer.OnCompletedAsync();
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnErrorAsync_WhenObserverInitializedWithOnErrorCallback_ShouldTriggerAndAwaitCallback()
    {
        // Arrange
        var triggered = false;
        AsyncObserver<bool> observer = new(
            onError: async (_) =>
            {
                await Task.Delay(100);
                triggered = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Act / Assert
        var task = observer.OnErrorAsync(null!);
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }
}
