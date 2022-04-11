using MassTransit;

namespace Library.Components;

public class BookStateMachine : MassTransitStateMachine<Book>
{
    public BookStateMachine()
    {
        Event(() => Added, x => x.CorrelateById(m => m.Message.BookId));
    }

    public Event<BookAdded> Added { get; }
}