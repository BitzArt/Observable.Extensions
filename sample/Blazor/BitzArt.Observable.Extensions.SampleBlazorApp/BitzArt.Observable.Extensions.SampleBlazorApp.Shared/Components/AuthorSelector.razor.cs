using Microsoft.AspNetCore.Components;

namespace BitzArt.Observable.Extensions.SampleBlazorApp;

public partial class AuthorSelector : ComponentBase
{
    [Parameter]
    public EventCallback<Author> ValueChanged { get; set; }

    private readonly List<Author> _values;

    private int _valueId;
    private Author _value;

    public AuthorSelector()
    {
        _values = SampleData.Authors.ToList();
        _value = _values.First();
        _valueId = _value.Id!.Value;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await ValueChanged.InvokeAsync(_value);
    }

    private async Task OnSelectedAsync(int id)
    {
        _valueId = id;
        _value = _values.First(x => x.Id == id);
        await ValueChanged.InvokeAsync(_value);
    }
}
