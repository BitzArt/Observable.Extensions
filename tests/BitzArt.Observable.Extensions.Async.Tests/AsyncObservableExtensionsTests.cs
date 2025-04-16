namespace BitzArt.Observable.Extensions.Async.Tests;

public class AsyncObservableExtensionsTests
{
    [Fact]
    public void Subscribe_WhenCallbacksPassed_ShouldReturnCorrectObservableSubscriptionResult()
    {
        // Arrange
        // Arrange
        var expectedResult = new TestDisposable();
        IAsyncObservable<bool> observable = new TestAsyncObservable<bool>(_ => expectedResult);

        // Act
        var result = observable.Subscribe(async (_)
            => await Task.CompletedTask);

        // Assert
        Assert.NotNull(result);
        Assert.Same(expectedResult, result);
    }

    private class TestAsyncObservable<T> : IAsyncObservable<T>
    {
        private readonly Func<IObserver<T>, IDisposable> _subscribe;

        public TestAsyncObservable(Func<IObserver<T>, IDisposable> subscribe)
        {
            _subscribe = subscribe;
        }

        public IDisposable Subscribe(IObserver<T> observer)
            => _subscribe(observer);
    }

    private class TestDisposable : IDisposable
    {
        public void Dispose() => throw new NotImplementedException();
    }
}
