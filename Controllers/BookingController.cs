using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using Marten.Events;
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
            using var session = _store.LightweightSession();
            
            var booking = new Booking 
            {  
                SpotId = spotId,
                CustomerId = customerId
            };

            session.Store(booking);

            session.SaveChanges();

            return booking;
        }
    }
}
