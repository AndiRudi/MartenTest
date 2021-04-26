using System;
using MartenTest.Aggregates;

namespace MartenTest.Events
{
    public class BookingCancelled : BookingUpdate
    {
        public override void Apply(Booking booking)
        {
            booking.Cancelled = true;
        }

        public override string ToString()
        {
            return $"{Time} Cancelled booking for customer {CustomerId} and spot {SpotId}";
        }
    }
}