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
    public void OnCompletedAsync_WhenObserverSubscribed_ShouldTriggerOnCompletedInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(
            onCompleted: () =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        // Act
        _ = observable.OnCompletedAsync();

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public void OnErrorAsync_WhenObserverSubscribed_ShouldTriggerOnErrorInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(
            onError: (_) =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        // Act
        _ = observable.OnErrorAsync(new Exception());

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnNextAsync_WhenAsyncObserverSubscribed_ShouldTriggerAndAwaitOnNextInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(onNext: async (_) =>
        {
            await Task.Delay(100);
            triggered = true;
        });

        // Act / Assert
        var task = observable.OnNextAsync(true);
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnCompletedAsync_WhenAsyncObserverSubscribed_ShouldTriggerAndAwaitOnCompletedInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

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
    public async Task OnErrorAsync_WhenAsyncObserverSubscribed_ShouldTriggerAndAwaitOnErrorInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

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
    public void OnNextAsync_WhenSubscribedObserverDisposed_ShouldNotTriggerOnNextInObserver()
    {
        // Arrange
        bool triggered = false;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onNext: (value) =>
            {
                triggered = true;
            });

        subscription.Dispose();

        // Act
        _ = observable.OnNextAsync(true);

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public void OnCompletedAsync_WhenSubscribedObserverDisposed_ShouldNotTriggerOnCompletedInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onCompleted: () =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        subscription.Dispose();

        // Act
        _ = observable.OnCompletedAsync();

        // Assert
        Assert.False(triggered);
    }

    [Fact]
    public void OnErrorAsync_WhenSubscribedObserverDisposed_ShouldNotTriggerOnErrorInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onError: (_) =>
            {
                triggered = true;
            },
            onNext: (_) => { });

        subscription.Dispose();

        // Act
        _ = observable.OnErrorAsync(new Exception());

        // Assert
        Assert.False(triggered);
    }

    [Fact]
    public async Task OnNextAsync_WhenSubscribedAsyncObserverDisposed_ShouldNotTriggerOnNextInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

        var subsciption = observable.Subscribe(onNext: async (_) =>
        {
            await Task.Delay(100);
            triggered = true;
        });

        subsciption.Dispose();

        // Act / Assert
        var task = observable.OnNextAsync(true);
        Assert.False(triggered);

        await task;
        Assert.False(triggered);
    }

    [Fact]
    public async Task OnCompletedAsync_WhenSubscribedAsyncObserverDisposed_ShouldNotTriggerOnCompletedInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onCompleted: async () =>
            {
                await Task.Delay(100);
                triggered = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        subscription.Dispose();

        // Act / Assert
        var task = observable.OnCompletedAsync();
        Assert.False(triggered);

        await task;
        Assert.False(triggered);
    }

    [Fact]
    public async Task OnErrorAsync_WhenSubscribedAsyncObserverDisposed_ShouldNotTriggerOnErrorInObserver()
    {
        // Arrange
        var triggered = false;
        AsyncObservable<bool> observable = new();

        var subscription = observable.Subscribe(
            onError: async (_) =>
            {
                await Task.Delay(100);
                triggered = true;
            },
            onNext: async (_) => await Task.CompletedTask);

        subscription.Dispose();

        // Act / Assert
        var task = observable.OnErrorAsync(new Exception());
        Assert.False(triggered);

        await task;
        Assert.False(triggered);
    }

    [Fact]
    public async Task OnNextAsync_WhenObservableDisposed_ShouldThrow()
    {
        // Arrange
        AsyncObservable<bool> observable = new();
        observable.Dispose();

        // Act / Assert
        await Assert.ThrowsAnyAsync<Exception>(()
            => observable.OnNextAsync(true));
    }

    [Fact]
    public async Task OnCompletedAsync_WhenObservableDisposed_ShouldThrow()
    {
        // Arrange
        AsyncObservable<bool> observable = new();
        observable.Dispose();

        // Act / Assert
        await Assert.ThrowsAnyAsync<Exception>(()
            => observable.OnCompletedAsync());
    }

    [Fact]
    public async Task OnErrorAsync_WhenObservableDisposed_ShouldThrow()
    {
        // Arrange
        AsyncObservable<bool> observable = new();
        observable.Dispose();

        // Act / Assert
        await Assert.ThrowsAnyAsync<Exception>(()
            => observable.OnErrorAsync(new Exception()));
    }
}
