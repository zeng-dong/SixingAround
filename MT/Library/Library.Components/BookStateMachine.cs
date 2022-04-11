using MassTransit;

namespace Library.Components;

public class BookStateMachine : MassTransitStateMachine<Book>
{
    public BookStateMachine()
    {
        Event(() => Added, x => x.CorrelateById(m => m.Message.BookId));

        InstanceState(x => x.CurrentState, Available);

        Initially(
            When(Added)
                .CopyDataToInstance()
                .TransitionTo(Available));
    }

    public Event<IBookAdded> Added { get; }

    public State Available { get; }
}

public static class BookStateMachineExtensions
{
    public static EventActivityBinder<Book, IBookAdded> CopyDataToInstance(this EventActivityBinder<Book, IBookAdded> binder)
    {
        return binder.Then(x =>
        {
            x.Saga.DateAdded = x.Message.Timestamp.Date;
            x.Saga.Title = x.Message.Title;
            x.Saga.Isbn = x.Message.Isbn;
        });
    }
}