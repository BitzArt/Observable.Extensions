![Tests](https://github.com/BitzArt/Observable.Extensions/actions/workflows/Tests.yml/badge.svg)

[![NuGet version](https://img.shields.io/nuget/v/BitzArt.Observable.Extensions.svg)](https://www.nuget.org/packages/BitzArt.Observable.Extensions/)
[![NuGet downloads](https://img.shields.io/nuget/dt/BitzArt.Observable.Extensions.svg)](https://www.nuget.org/packages/BitzArt.Observable.Extensions/)

## Overview

**BitzArt.Observable.Extensions** is a nuget package that provides various extensions of IObservable functionality.

### Installation

- Install the following package in your project:

```
dotnet add package BitzArt.Observable.Extensions
```

### Usage

The following example shows how initialize an observable and notify its subscribers:

```csharp
using BitzArt;
...

public AsyncObservable<bool> Observable = new();
...

await Observable.OnNextAsync(true);

```

The following example shows how to subscribe to an observable to receive notifications:

```csharp
using BitzArt;
...

public class YourObservableConsumer()
{
	private IAsyncObservable<bool> _observable;

	public ObservableConsumer(IAsyncObservable<bool> observable)
	{
		_observable = observable;
		
		_observable.Subscribe(async (value) =>
		{
			// Async operation
		});
	}
}

```

## License

[![License](https://img.shields.io/badge/mit-%230072C6?style=for-the-badge)](https://github.com/BitzArt/Observable.Extensions/blob/main/LICENSE)
