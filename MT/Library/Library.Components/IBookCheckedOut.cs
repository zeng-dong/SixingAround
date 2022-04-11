namespace Library.Components;

public interface IBookCheckedOut
{
    Guid CheckOutId { get; }

    DateTime Timestamp { get; }

    Guid MemberId { get; }

    Guid BookId { get; }
}