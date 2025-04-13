using System.Threading.Tasks;

namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableTests
{
    [Fact]
    public async Task NotifyObserversAsync_WhenCalled_ShouldNotifyObservers()
    {
        // Arrange
        AsyncObservable<int?> observable = default;
        var observer = new AsyncObserver<int?>(observable, DoOperationAsync);
        var notified = false;

        Task DoOperationAsync(int? value)
        {
            notified = true;
            return Task.CompletedTask;
        }

        // Act
        await observable.NotifyObserversAsync(1);

        // Assert
        Assert.True(notified);
    }
}
