using System;
using MartenTest.Events;

namespace MartenTest.Aggregates
{
    public abstract class Spot
    {
        public Guid Id { get; set; }

        //Type of Spot (Calendar based, Seat based, Appointment)
        //Spots are created the moment the booking is opened for those spots
        //The moment as spot exists, it can be reserved

        public Spot()
        {

        }

        public abstract void Apply(SpotUpdate spotUpdate);
    }
}