using System;

namespace BitzArt.Observable.Extensions.Tests;

public class ObservableExtensionsTests
{
    [Fact]
    public void Subscribe_WhenCalled_ShouldReturnDisposable()
    {
        // Arrange
        IObservable<bool> observable = new AsyncObservable<bool>();

        // Act
        var disposable = observable.Subscribe((_) => { });

        // Assert
        Assert.NotNull(disposable);
        Assert.IsAssignableFrom<IDisposable>(disposable);
    }
}
