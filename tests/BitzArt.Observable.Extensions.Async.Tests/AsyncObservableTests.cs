namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableTests
{
    [Fact]
    public void OnNextAsync_WhenObserverSubscribed_ShouldTriggerOnNextInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool?>(
            onNext: (_) =>
            {
                triggered = true;
            });

        // Act
        _ = sut.Observable.OnNextAsync(null!);

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnNextAsync_WhenAsyncObserverSubscribed_ShouldTriggerAndAwaitOnNextInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool?>(
            onNext: async (_) =>
            {
                await Task.Delay(100);
                triggered = true;
            });

        // Act / Assert
        var task = sut.Observable.OnNextAsync(null!);
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public void OnNextAsync_WhenSubscribedObserverDisposed_ShouldNotTriggerOnNextInObserver()
    {
        // Arrange
        bool triggered = false;

        var sut = new AsyncObservableSut<bool?>(
            onNext: (_) =>
            {
                triggered = true;
            });

        sut.Subscription.Dispose();

        // Act
        _ = sut.Observable.OnNextAsync(null!);

        // Assert
        Assert.False(triggered);
    }

    [Fact]
    public async Task OnNextAsync_WhenSubscribedAsyncObserverDisposed_ShouldNotTriggerOnNextInObserver()
    {
        // Arrange
        var triggered = false;
        
        var sut = new AsyncObservableSut<bool?>(onNext: async (_) =>
        {
            await Task.Delay(100);
            triggered = true;
        });

        sut.Subscription.Dispose();

        // Act / Assert
        var task = sut.Observable.OnNextAsync(null!);
        Assert.False(triggered);

        await task;
        Assert.False(triggered);
    }

    [Fact]
    public void OnCompletedAsync_WhenObserverSubscribed_ShouldTriggerOnCompletedInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool>(
            onCompleted: () =>
            {
                triggered = true;
            });

        // Act
        _ = sut.Observable.OnCompletedAsync();

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnCompletedAsync_WhenAsyncObserverSubscribed_ShouldTriggerAndAwaitOnCompletedInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool>(
            onCompleted: async () =>
            {
                await Task.Delay(100);
                triggered = true;
            });

        // Act / Assert
        var task = sut.Observable.OnCompletedAsync();
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public void OnCompletedAsync_WhenSubscribedObserverDisposed_ShouldNotTriggerOnCompletedInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool>(
            onCompleted: () =>
            {
                triggered = true;
            });

        sut.Subscription.Dispose();

        // Act
        _ = sut.Observable.OnCompletedAsync();

        // Assert
        Assert.False(triggered);
    }

    [Fact]
    public async Task OnCompletedAsync_WhenSubscribedAsyncObserverDisposed_ShouldNotTriggerOnCompletedInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool>(
            onCompleted: async () =>
            {
                await Task.Delay(100);
                triggered = true;
            });

        sut.Subscription.Dispose();

        // Act / Assert
        var task = sut.Observable.OnCompletedAsync();
        Assert.False(triggered);

        await task;
        Assert.False(triggered);
    }

    [Fact]
    public void OnErrorAsync_WhenObserverSubscribed_ShouldTriggerOnErrorInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool>(
            onError: (_) =>
            {
                triggered = true;
            });

        // Act
        _ = sut.Observable.OnErrorAsync(null!);

        // Assert
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnErrorAsync_WhenAsyncObserverSubscribed_ShouldTriggerAndAwaitOnErrorInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool>(
            onError: async (_) =>
            {
                await Task.Delay(100);
                triggered = true;
            });

        // Act / Assert
        var task = sut.Observable.OnErrorAsync(null!);
        Assert.False(triggered);

        await task;
        Assert.True(triggered);
    }

    [Fact]
    public void OnErrorAsync_WhenSubscribedObserverDisposed_ShouldNotTriggerOnErrorInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool>(
            onError: (_) =>
            {
                triggered = true;
            });

        sut.Subscription.Dispose();

        // Act
        _ = sut.Observable.OnErrorAsync(null!);

        // Assert
        Assert.False(triggered);
    }

    [Fact]
    public async Task OnErrorAsync_WhenSubscribedAsyncObserverDisposed_ShouldNotTriggerOnErrorInObserver()
    {
        // Arrange
        var triggered = false;

        var sut = new AsyncObservableSut<bool>(
            onError: async (_) =>
            {
                await Task.Delay(100);
                triggered = true;
            });

        sut.Subscription.Dispose();

        // Act / Assert
        var task = sut.Observable.OnErrorAsync(null!);
        Assert.False(triggered);

        await task;
        Assert.False(triggered);
    }

    private record AsyncObservableSut<T>
    {
        public AsyncObservable<T> Observable { get; } = new();

        public IDisposable Subscription { get; }

        public AsyncObservableSut(Action<T>? onNext = null, Action? onCompleted = null, Action<Exception>? onError = null)
        {
            onNext ??= (_) => { };
            Subscription = Observable.Subscribe(onNext, onCompleted, onError);
        }

        public AsyncObservableSut(Func<T, Task>? onNext = null, Func<Task>? onCompleted = null, Func<Exception, Task>? onError = null)
        {
            onNext ??= (_) => Task.CompletedTask;
            Subscription = Observable.Subscribe(onNext, onCompleted, onError);
        }
    }
}
