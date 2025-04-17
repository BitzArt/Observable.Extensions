namespace BitzArt.Observable.Extensions.SampleBlazorApp;

public static class SampleData
{
    public static Author[] Authors =
    {
        new() { Id = 1, Name = "Stephen King" },
        new() { Id = 2, Name = "J.K. Rowling" },
        new() { Id = 3, Name = "George Orwell" }
    };

    public static Book[] Books =
    {
        new() { Id = 1, AuthorId = 1, Title = "The Shining" },
        new() { Id = 2, AuthorId = 1, Title = "It" },
        new() { Id = 3, AuthorId = 2, Title = "Harry Potter and the Philosopher's Stone" },
        new() { Id = 4, AuthorId = 2, Title = "Harry Potter and the Chamber of Secrets" },
        new() { Id = 5, AuthorId = 3, Title = "1984" },
        new() { Id = 6, AuthorId = 3, Title = "Animal Farm" }
    };
}
