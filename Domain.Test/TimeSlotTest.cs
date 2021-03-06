using System;
using DevChallenge.Domain;
using NUnit.Framework;
using Shouldly;

namespace Domain.Tests
{
    public class TimeSlotTest
    {

        [Test]
        public void DateTimeToTimeSlotWhenTimeBeforeEpochStartPassedToCtorExceptionShouldBeThrown()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => TimeSlot.DateTimeToTimeSlot(new DateTime(2000,1,1)));
        }

        [Test]
        public void DateTimeToTimeSlotValidUTCTimePassedNumberReturned()
        {
            TimeSlot.DateTimeToTimeSlot(new DateTime(2018,03,15,17,34,50,DateTimeKind.Utc)).ShouldBe(637);
        }

        [Test]
        public void DateTimeToTimeSlotWhenEpochStartTimePassedNumberShouldBeZero()
        {
            TimeSlot.DateTimeToTimeSlot(TimeSlot.EpochStart).ShouldBe(0);
        }


    }
}