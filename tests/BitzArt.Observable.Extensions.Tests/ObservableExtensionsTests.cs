namespace BitzArt.Observable.Extensions.Tests;

public class ObservableExtensionsTests
{
    [Fact]
    public async Task Subscribe_WhenOnNextCallbackPassed_ShouldStartTrigger()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var triggered = false;

        // Act
        observable.Subscribe(onNext: (_) =>
        {
            triggered = true;
        });

        // Assert
        await observable.OnNextAsync(true);
        Assert.True(triggered);
    }

    [Fact]
    public async Task Subscribe_WhenOnCompletedCallbackPassed_ShouldStartTrigger()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var triggered = false;

        // Act
        observable.Subscribe(
            onCompleted: () =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        // Assert
        await observable.OnCompletedAsync();
        Assert.True(triggered);
    }

    [Fact]
    public async Task Subscribe_WhenOnErrorCallbackPassed_ShouldStartTrigger()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var triggered = false;

        // Act
        observable.Subscribe(
            onError: (_) =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        // Assert
        await observable.OnErrorAsync(null!);
        Assert.True(triggered);
    }
}
