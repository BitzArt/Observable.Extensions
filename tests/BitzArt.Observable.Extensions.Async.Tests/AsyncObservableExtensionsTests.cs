namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableExtensionsTests
{
    [Fact]
    public void Subscribe_WhenCalled_ShouldReturnSubscription()
    {
        // Arrange
        IAsyncObservable<bool> observable = new AsyncObservable<bool>();

        // Act
        var subscription = observable.Subscribe(async (_)
            => await Task.CompletedTask);

        // Assert
        Assert.NotNull(subscription);
    }
}
