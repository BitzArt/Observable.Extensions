using Microsoft.AspNetCore.Components;

namespace BitzArt.Observable.Extensions.SampleBlazorApp;

public partial class BooksPage : ComponentBase
{
    private readonly AsyncObservable<Author> _authorObservable = new();

    private readonly List<Author> _authors = [.. SampleData.Authors];

    private List<Book> _books = [];

    private bool _isLoading = false;

    private async Task OnAuthorSelectedAsync(ChangeEventArgs args)
    {
        var id = Int32.Parse(args.Value!.ToString()!);
        var author = _authors.First(x => x.Id == id);

        await _authorObservable.OnNextAsync(author);
    }

    protected override void OnInitialized()
    {
        _authorObservable.Subscribe(async (author) =>
        {
            _isLoading = true;
            _books = await GetBooksAsync(author.Id!.Value);
            _isLoading = false;

            await InvokeAsync(StateHasChanged);
        });
    }

    private static async Task<List<Book>> GetBooksAsync(int authorId)
    {
        // Simulate async operation
        await Task.Delay(500);
        return [.. SampleData.Books.Where(x => x.AuthorId == authorId)];
    }
}
