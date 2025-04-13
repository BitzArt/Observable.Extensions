using Microsoft.AspNetCore.Components;

namespace BitzArt.Observable.Extensions.SampleBlazorApp;

public partial class BooksList : ComponentBase, IDisposable
{
    [Parameter]
    public AsyncObservable<Author> AuthorObservable { get; set; }

    private AsyncObserver<Author?> _authorObserver = null!;

    private List<Book> _books = [];

    private bool _isLoading = false;

    protected override void OnInitialized()
    {
        _authorObserver = new(AuthorObservable, onNext: OnAuthorChangedAsync);
    }

    private async Task OnAuthorChangedAsync(Author? author)
    {
        _books = await GetBooksAsync(author?.Id);
        await InvokeAsync(StateHasChanged);
    }

    private async Task<List<Book>> GetBooksAsync(int? authorId)
    {
        _isLoading = true;
        await Task.Delay(500); // Simulate async operation
        _isLoading = false;

        return SampleData.Books
            .Where(x => x.AuthorId == authorId)
            .ToList();
    }

    public void Dispose()
    {
        _authorObserver.Dispose();
    }
}
