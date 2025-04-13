using Microsoft.AspNetCore.Components;

namespace BitzArt.Observable.Extensions.SampleBlazorApp;

public partial class BooksPage : ComponentBase
{
    private AsyncObservable<Author> _authorObservable = new();

    private async Task OnAuthorSelectedAsync(Author author)
    {
        await _authorObservable.OnNextAsync(author);
    }
}
