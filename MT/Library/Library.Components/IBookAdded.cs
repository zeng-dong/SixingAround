namespace Library.Components;

public interface IBookAdded
{
    Guid BookId { get; }

    DateTime Timestamp { get; }

    string Isbn { get; }
    string Title { get; }
}