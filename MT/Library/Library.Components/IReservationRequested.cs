using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Components;

public interface IReservationRequested
{
    Guid ReservationId { get; }

    DateTime Timestamp { get; }

    TimeSpan? Duration { get; }

    Guid MemberId { get; }

    Guid BookId { get; }
}