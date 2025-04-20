namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableTests
{
    [Fact]
    public async Task OnNextAsync_WhenHasSubscriber_ShouldNotifySubscriber()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var triggered = false;

        observable.Subscribe(
            onNext: (_) =>
            {
                triggered = true;
            });

        // Act
        await observable.OnNextAsync(null!);

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnCompletedAsync_WhenHasSubscriber_ShouldNotifySubscriber()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var triggered = false;

        observable.Subscribe(
            onCompleted: () =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        // Act
        await observable.OnCompletedAsync();

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnErrorAsync_WhenHasSubscriber_ShouldNotifySubscriber()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var triggered = false;

        observable.Subscribe(
            onError: (_) =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        // Act
        await observable.OnErrorAsync(new Exception());

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnNextAsync_WhenHasAsyncSubscriber_ShouldAwait()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var triggered = false;

        observable.Subscribe(
            onNext: async (_) =>
            {
                await Task.Delay(100);
                triggered = true;
            });

        // Act / Assert
        var task = observable.OnNextAsync(null!);
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnCompletedAsync_WhenHasAsyncSubscriber_ShouldAwait()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var triggered = false;

        observable.Subscribe(
            onCompleted: async () =>
            {
                await Task.Delay(100);
                triggered = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Act / Assert
        var task = observable.OnCompletedAsync();
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnErrorAsync_WhenHasAsyncSubscriber_ShouldAwait()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var triggered = false;

        observable.Subscribe(
            onError: async (_) =>
            {
                await Task.Delay(100);
                triggered = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        // Act / Assert
        var task = observable.OnErrorAsync(new Exception());
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public async Task Unsubscription_AfterSubscribed_SubscriberShouldStopReceivingNotifications()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var triggered = false;

        var subscription = observable.Subscribe(
            onNext: (_) =>
            {
                triggered = true;
            });

        // Act
        subscription.Dispose();

        // Assert
        await observable.OnNextAsync(null!); // Consider to use a different way to check this case
        Assert.False(triggered);
    }

    [Fact]
    public void Unsubscription_WhenAlreadyUnsubscribed_ShouldThrowObjectDisposedException()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var subscription = observable.Subscribe(onNext: (_) => { });
        subscription.Dispose();

        // Act / Assert
        Assert.ThrowsAny<ObjectDisposedException>(() => subscription.Dispose());
    }

    [Fact]
    public async Task OnNextAsync_AfterUnsubscribed_ShouldNotNotify()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var triggered = false;

        var subscription = observable.Subscribe(
            onNext: (_) =>
            {
                triggered = true;
            });

        subscription.Dispose();

        // Act
        await observable.OnNextAsync(null!);

        // Assert
        Assert.False(triggered);
    }

    [Fact]
    public async Task OnCompletedAsync_AfterUnsubscribed_ShouldNotNotify()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();

        var triggered = false;
        var subscription = observable.Subscribe(
            onCompleted: () =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        subscription.Dispose();

        // Act
        await observable.OnCompletedAsync();

        // Assert
        Assert.False(triggered);
    }

    [Fact]
    public async Task OnErrorAsync_AfterUnsubscribed_ShouldNotNotify()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        var triggered = false;

        var subscription = observable.Subscribe(
            onError: (_) =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        subscription.Dispose();

        // Act
        await observable.OnErrorAsync(new Exception());

        // Assert
        Assert.False(triggered);
    }

    [Fact]
    public async Task Dispose_WhenNotPreviouslyDisposed_OnNextShouldThrowException()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        observable.Subscribe(onNext: (_) => { });

        // Act
        observable.Dispose();

        // Assert
        await Assert.ThrowsAsync<ObjectDisposedException>(async ()
            => await observable.OnNextAsync(null!));
    }

    [Fact]
    public async Task Dispose_WhenNotPreviouslyDisposed_OnCompletedShouldThrowException()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        observable.Subscribe(onNext: (_) => { });

        // Act
        observable.Dispose();

        // Assert
        await Assert.ThrowsAsync<ObjectDisposedException>(async ()
            => await observable.OnCompletedAsync());
    }

    [Fact]
    public async Task Dispose_WhenNotPreviouslyDisposed_OnErrorShouldThrowException()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        observable.Subscribe(onNext: (_) => { });

        // Act
        observable.Dispose();

        // Assert
        await Assert.ThrowsAsync<ObjectDisposedException>(async ()
            => await observable.OnErrorAsync(new Exception()));
    }

    [Fact]
    public void Dispose_WhenAlreadyDisposed_ShouldThrowObjectDisposedException()
    {
        // Arrange
        var observable = new AsyncObservable<bool?>();
        observable.Subscribe(onNext: (_) => { });

        observable.Dispose();

        // Act / Assert
        Assert.Throws<ObjectDisposedException>(() => observable.Dispose());
    }
}
