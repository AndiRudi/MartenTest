# MartenTest
Test of Marten DocumentDb

The booking system ensures that a customer can book a spot. Spots are atomically bookable entities. Spots can be created by external systems. 

Example: A calendar system can create spots which are seats in an event. The event itself is stored inside the event system, but once an event becomes bookable the event systems calls into the booking system to create the set amount of seats at the events date.

Example: In a video application a video can be booked by a customer for a specific time frame. The spot is created by the video application for the video on request. That means as part of the booking process the video application has to create a new spot first, then book that spot.

Example: An appointment system may create appointments which are for one customer and one trainer at a specific time inside a time range, but not overlapping. The appointment system needs to ensure that a spot is created before the booking which is not overlapping.
