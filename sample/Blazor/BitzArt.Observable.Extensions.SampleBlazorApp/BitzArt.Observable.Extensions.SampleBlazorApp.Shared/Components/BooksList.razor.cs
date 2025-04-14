using Microsoft.AspNetCore.Components;

namespace BitzArt.Observable.Extensions.SampleBlazorApp;

public partial class BooksList : ComponentBase, IDisposable
{
    [Parameter]
    public AsyncObservable<Author> AuthorObservable { get; set; } = null!;

    private IDisposable _authorObserver = null!;

    private List<Book> _books = [];

    private bool _isLoading = false;

    protected override void OnInitialized()
    {
        _authorObserver = AuthorObservable.Subscribe(async (author) =>
        {
            _isLoading = true;
            _books = await GetBooksAsync(author?.Id);
            _isLoading = false;

            await InvokeAsync(StateHasChanged);
        });
    }

    private static async Task<List<Book>> GetBooksAsync(int? authorId)
    {
        // Simulate async operation
        await Task.Delay(500);

        return SampleData.Books
            .Where(x => x.AuthorId == authorId)
            .ToList();
    }

    public void Dispose()
    {
        _authorObserver.Dispose();
        GC.SuppressFinalize(this);
    }
}
