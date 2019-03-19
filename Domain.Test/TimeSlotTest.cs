using System;
using DevChallenge.Domain;
using NUnit.Framework;
using Shouldly;

namespace Domain.Tests
{
    public class TimeSlotTest
    {
        [Test]
        public void WhenLocalTimePassedExceptionThrown()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => new TimeSlot(new DateTime(2001, 1, 1, 10, 10, 10, DateTimeKind.Local)));
        }

        [Test]
        public void WhenUnspecifiedTimePassedExceptionThrown()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => new TimeSlot(new DateTime(2001, 1, 1, 10, 10, 10, DateTimeKind.Unspecified)));
        }

        [Test]
        public void WhenTimeBeforeEpochStartPassedToCtorExceptionShouldBeThrown()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => new TimeSlot(new DateTime(2000,1,1)));
        }

        [Test]
        public void ValidUTCTimePassedNumberReturned()
        {
            var sut = new TimeSlot(new DateTime(2018,03,15,17,34,50,DateTimeKind.Utc));
            sut.Number.ShouldBe(637);
        }

        [Test]
        public void WhenEpochStartTimePassedNumberShouldBeZero()
        {
            var sut = new TimeSlot(TimeSlot.EpochStart);
            sut.Number.ShouldBe(0); 
        }


    }
}