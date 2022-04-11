namespace Library.Components;

public interface IBookReservationCanceled
{
    Guid BookId { get; }

    Guid ReservationId { get; }
}