using System;
using System.Threading.Tasks;

namespace BitzArt.Observable.Extensions.Tests;

public class AsyncObservableExtensionsTests
{
    [Fact]
    public void Subscribe_WhenCalled_ShouldReturnDisposable()
    {
        // Arrange
        IAsyncObservable<bool> observable = new AsyncObservable<bool>();

        // Act
        var disposable = observable.Subscribe(async (_) 
            => await Task.CompletedTask);

        // Assert
        Assert.NotNull(disposable);
        Assert.IsAssignableFrom<IDisposable>(disposable);
    }
}
