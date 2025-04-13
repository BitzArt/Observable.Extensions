using System.Threading;
using System.Threading.Tasks;

namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableTests
{
    [Fact]
    public void Constructor_WhenCalled_ShouldInitializeAsyncObservable()
    {
        IAsyncObservable<int> siteIdObservable = null!;

        var siteIdUnsuscriber = siteIdObservable.Subscribe((value) =>
        {
            var siteId = value;
        });
    }

    [Fact]
    public async Task NotifyObserversAsync_WhenCalled_ShouldNotifyObservers()
    {
        // Arrange
        var notified = false;

        AsyncObservable<int?> observable = new();

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        observable.Subscribe(async (_) =>
        {
            notified = true;
            await Task.Delay(0, cancellationToken);
        });

        cts.Cancel();

        // Act
        await observable.OnNextAsync(1);

        // Assert
        Assert.True(notified);
    }
}
