using System;

namespace MartenTest.Events
{
    public class BookingCreated
    {
        public BookingCreated()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public string SpotId { get; set; }
        public string CustomerId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public override string ToString()
        {
            return $"{CreatedAt} Created booking for customer {CustomerId} and spot {SpotId}";
        }
    }
}