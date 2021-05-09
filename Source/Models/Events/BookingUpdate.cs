using System;
using MartenTest.Aggregates;

namespace MartenTest.Events
{
    public abstract class BookingUpdate
    {
        public string SpotId { get; set; }
        public string CustomerId { get; set; }
        public DateTimeOffset Time { get; set; }

        public BookingUpdate()
        {
            Time = DateTimeOffset.UtcNow;
        }

        public abstract void Apply(Booking booking);
    }
}
