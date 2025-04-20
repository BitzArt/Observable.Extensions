namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableExtensionsTests
{
    [Fact]
    public async Task Subscribe_WhenOnNextCallbackPassed_ShouldAwaitCallback()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var triggered = false;

        // Act
        observable.Subscribe(onNext: async (_) =>
        {
            await Task.Delay(100);
            triggered = true;
        });

        // Assert
        var task = observable.OnNextAsync(true);
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public async Task Subscribe_WhenOnCompletedCallbackPassed_ShouldAwaitCallback()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var triggered = false;

        // Act
        observable.Subscribe(
            onCompleted: async () =>
            {
                await Task.Delay(100);
                triggered = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Assert
        var task = observable.OnCompletedAsync();
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public async Task Subscribe_WhenOnErrorCallbackPassed_ShouldAwaitCallback()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var triggered = false;

        // Act
        observable.Subscribe(
            onError: async (_) =>
            {
                await Task.Delay(100);
                triggered = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Assert
        var task = observable.OnErrorAsync(null!);
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }
}
