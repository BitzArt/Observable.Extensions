namespace BitzArt.Observable.Extensions.Tests;

public class ObservableExtensionsTests
{
    [Fact]
    public void Subscribe_WhenCallbacksPassed_ShouldReturnCorrectObservableSubscriptionResult()
    {
        // Arrange
        var expectedResult = new TestDisposable();
        IObservable<bool> observable = new TestObservable<bool>(_ => expectedResult);

        // Act
        var result = observable.Subscribe((_) => { });

        // Assert
        Assert.NotNull(result);
        Assert.Same(expectedResult, result);
    }

    private class TestObservable<T> : IObservable<T>
    {
        private readonly Func<IObserver<T>, IDisposable> _subscribe;

        public TestObservable(Func<IObserver<T>, IDisposable> subscribe)
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
