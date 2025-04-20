namespace BitzArt.Observable.Extensions.Tests;

public class ObservableExtensionsTests
{
    [Fact]
    public async Task Subscribe_OnNextCallbackPassed_ShouldStartTrigger()
    {
        // Arrange
        var observable = new AsyncObservable<bool>();
        var trriggered = false;

        // Act
        observable.Subscribe(onNext: (_) =>
        {
            trriggered = true;
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
    public async Task Subscribe_OnErrorCallbackPassed_ShouldStartTrigger()
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
        await observable.OnErrorAsync(new Exception());
        Assert.True(triggered);
    }
}
