using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using Marten.Events;
using Marten.Util;
using MartenTest.Aggregates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MartenTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IDocumentStore _store;

        public BookingController(ILogger<BookingController> logger, IDocumentStore store)
        {
            _logger = logger;
            _store = store;
        }

        [HttpGet]
        public IEnumerable<Booking> Get()
        {
            using var session = _store.QuerySession();

            return session
                .Query<Booking>()
                .ToList();
        }

        [HttpPost]
        public Booking Create(string customerId, string spotId)
        {
            using var session = _store.OpenSession();

            var bookingCreated = new Events.BookingCreated
            {
                SpotId = spotId,
                CustomerId = customerId
            };

            var bookingId = Guid.NewGuid();
            session.Events.StartStream<Booking>(bookingId, bookingCreated);
            session.SaveChanges();

            return session
                .Query<Booking>()
                .First(b => b.Id == bookingId);
        }

        [HttpDelete]
        public Booking Delete(Guid bookingId)
        {
            using var session = _store.OpenSession();

            var booking = session
                .Query<Booking>()
                .First(b => b.Id == bookingId);

            var bookingCancelled = new Events.BookingCancelled
            {
                SpotId = booking.SpotId,
                CustomerId = booking.CustomerId
            };
            session.Events.Append(bookingId, bookingCancelled);
            session.SaveChanges();

            return session
                .Query<Booking>()
                .First(b => b.Id == bookingId);
        }
    }
}
