namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableTests
{
    [Fact]
    public void OnNextAsync_WhenObserverSubscribed_ShouldTriggerOnNextInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(onNext: (_) =>
        {
            triggered = true;
        });

        // Act
        _ = observable.OnNextAsync(true);

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public void OnCompletedAsync_WhenSyncSubscription_ShouldNotify()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(
            onCompleted: () =>
            {
                notified = true;
            },
            onNext: (_) => { });

        // Act
        _ = observable.OnCompletedAsync();

        // Assert
        Assert.True(notified);
    }

    [Fact]
    public void OnErrorAsync_WhenSyncSubscription_ShouldNotify()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(
            onError: (_) =>
            {
                notified = true;
            },
            onNext: (_) => { });

        // Act
        _ = observable.OnErrorAsync(new Exception());

        // Assert
        Assert.True(notified);
    }

    [Fact]
    public async Task OnNextAsync_WhenAsyncSubscription_ShouldNotifyAndAwait()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(onNext: async (_) =>
        {
            await Task.Delay(100);
            notified = true;
        });

        // Act / Assert
        var task = observable.OnNextAsync(true);
        Assert.False(notified);

        await task;
        Assert.True(notified);
    }

    [Fact]
    public async Task OnCompletedAsync_WhenAsyncSubscription_ShouldNotifyAndAwait()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(
            onCompleted: async () =>
            {
                await Task.Delay(100);
                notified = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Act / Assert
        var task = observable.OnCompletedAsync();
        Assert.False(notified);

        await task;
        Assert.True(notified);
    }

    [Fact]
    public async Task OnErrorAsync_WhenAsyncSubscription_ShouldNotifyAndAwait()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(
            onError: async (_) =>
            {
                await Task.Delay(100);
                notified = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Act / Assert
        var task = observable.OnErrorAsync(new Exception());
        Assert.False(notified);

        await task;
        Assert.True(notified);
    }

    [Fact]
    public async Task OnNextAsync_WhenDisposed_ShouldThrow()
    {
        // Arrange
        AsyncObservable<bool> observable = new();
        observable.Dispose();

        // Act / Assert
        await Assert.ThrowsAnyAsync<ObjectDisposedException>(()
            => observable.OnNextAsync(true));
    }

    [Fact]
    public async Task OnCompletedAsync_WhenDisposed_ShouldThrow()
    {
        // Arrange
        AsyncObservable<bool> observable = new();
        observable.Dispose();

        // Act / Assert
        await Assert.ThrowsAnyAsync<ObjectDisposedException>(()
            => observable.OnCompletedAsync());
    }

    [Fact]
    public async Task OnErrorAsync_WhenDisposed_ShouldThrow()
    {
        // Arrange
        AsyncObservable<bool> observable = new();
        observable.Dispose();

        // Act / Assert
        await Assert.ThrowsAnyAsync<ObjectDisposedException>(()
            => observable.OnErrorAsync(new Exception()));
    }

    [Fact]
    public void OnNextAsync_WhenSubscribedObserverDisposed_ShouldNotTriggerOnNextInObserver()
    {
        // Arrange
        bool expectedValue = true;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onNext: (value) =>
            {
                expectedValue = value;
            });

        subscription.Dispose();

        // Act
        _ = observable.OnNextAsync(false);

        // Assert
        Assert.True(expectedValue);
    }

    [Fact]
    public void OnCompletedAsync_WhenSyncSubscriptionDisposed_ShouldNotNotify()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onCompleted: () =>
            {
                notified = true;
            },
            onNext: (_) => { });

        subscription.Dispose();

        // Act
        _ = observable.OnCompletedAsync();

        // Assert
        Assert.False(notified);
    }

    [Fact]
    public void OnErrorAsync_WhenSyncSubscriptionDisposed_ShouldNotNotify()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onError: (_) =>
            {
                notified = true;
            },
            onNext: (_) => { });

        subscription.Dispose();

        // Act
        _ = observable.OnErrorAsync(new Exception());

        // Assert
        Assert.False(notified);
    }

    [Fact]
    public async Task OnNextAsync_WhenAsyncSubscriptionDisposed_ShouldNotNotify()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        var subsciption = observable.Subscribe(onNext: async (_) =>
        {
            await Task.Delay(100);
            notified = true;
        });

        subsciption.Dispose();

        // Act / Assert
        var task = observable.OnNextAsync(true);
        Assert.False(notified);

        await task;
        Assert.False(notified);
    }

    [Fact]
    public async Task OnCompletedAsync_WhenAsyncSubscriptionDisposed_ShouldNotNotify()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onCompleted: async () =>
            {
                await Task.Delay(100);
                notified = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        subscription.Dispose();

        // Act / Assert
        var task = observable.OnCompletedAsync();
        Assert.False(notified);

        await task;
        Assert.False(notified);
    }

    [Fact]
    public async Task OnErrorAsync_WhenAsyncSubscriptionDisposed_ShouldNotNotify()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onError: async (_) =>
            {
                await Task.Delay(100);
                notified = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        subscription.Dispose();

        // Act / Assert
        var task = observable.OnErrorAsync(new Exception());
        Assert.False(notified);

        await task;
        Assert.False(notified);
    }
}
