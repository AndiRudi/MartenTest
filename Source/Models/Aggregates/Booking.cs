using System;
using MartenTest.Events;

namespace MartenTest.Aggregates
{
    public abstract class Booking
    {
        public Guid Id { get; set; }
        public string SpotId { get; set; }
        public string CustomerId { get; set; }

        public bool Cancelled = false;
        public bool Reservation = false;

        public Booking()
        {

        }

        public abstract void Apply(BookingUpdate bookingUpdate);
    }
}