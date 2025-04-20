namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableExtensionsTests
{
    [Fact]
    public async Task Subscribe_OnNextCallbackPassed_ShouldStartTrigger()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var trriggered = false;

        // Act
        observable.Subscribe(onNext: async (_) =>
        {
            trriggered = true;
            await Task.CompletedTask;
        });

        // Assert
        await observable.OnNextAsync(true);
        Assert.True(trriggered);
    }

    [Fact]
    public async Task Subscribe_OnCompletedCallbackPassed_ShouldStartTrigger()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var triggered = false;

        // Act
        observable.Subscribe(
            onCompleted: async () =>
            {
                triggered = true;
                await Task.CompletedTask;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Assert
        await observable.OnCompletedAsync();
        Assert.True(triggered);
    }

    [Fact]
    public async Task Subscribe_OnErrorCallbackPassed_ShouldStartTrigger()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var triggered = false;

        // Act
        observable.Subscribe(
            onError: async (_) =>
            {
                triggered = true;
                await Task.CompletedTask;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Assert
        await observable.OnErrorAsync(new Exception());
        Assert.True(triggered);
    }
}
