using System;
using System.Threading.Tasks;

namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableTests
{
    [Fact]
    public void OnNextAsync_WhenSyncSubscription_ShouldNotify()
    {
        // Arrange
        var notified = false;
        AsyncObservable<bool> observable = new();

        observable.Subscribe(onNext: (_) =>
        {
            notified = true;
        });

        // Act
        _ = observable.OnNextAsync(true);

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
}
