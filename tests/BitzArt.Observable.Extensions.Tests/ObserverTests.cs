namespace BitzArt.Observable.Extensions.Tests;

public class ObserverTests
{
    [Fact]
    public void OnNext_WhenObserverInitializedWithOnNextCallback_ShouldTriggerCallback()
    {
        // Arrange
        var triggered = false;
        Observer<bool?> observer = new(
            onNext: (_) =>
            {
                triggered = true;
            });

        // Act
        observer.OnNext(null!);

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public void OnCompleted_WhenObserverInitializedWithOnCompletedCallback_ShouldTriggerCallback()
    {
        // Arrange
        var triggered = false;
        Observer<bool> observer = new(
            onCompleted: () =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        // Act
        observer.OnCompleted();

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public void OnError_WhenObserverInitializedWithOnErrorCallback_ShouldTriggerCallback()
    {
        // Arrange
        var triggered = false;
        Observer<bool> observer = new(
            onError: (_) =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        // Act
        observer.OnError(null!);

        // Assert
        Assert.True(triggered);
    }
}
