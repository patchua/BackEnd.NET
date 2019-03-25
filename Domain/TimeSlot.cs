using System;

namespace DevChallenge.Domain
{
    /// <summary>
    ///     Time slot is an interval of time from EpochStart in UTC
    /// </summary>
    public static class TimeSlot
    {
        /// <summary>
        ///     Length of one time slot in seconds
        /// </summary>
        public const long SlotLength = 10000;

        /// <summary>
        ///     Start DateTime(UTC) of first slot.
        /// </summary>
        public static readonly DateTime EpochStart = new DateTime(2018, 01, 01, 0, 0, 0, DateTimeKind.Utc);

        public static int DateTimeToTimeSlot(DateTime time)
        {
            if (time.Kind != DateTimeKind.Utc) throw new ArgumentOutOfRangeException(nameof(time));
            if (time < EpochStart) throw new ArgumentOutOfRangeException(nameof(time));
            return  (int) (time - EpochStart).TotalSeconds / 10000;
        }

        public static DateTime GetTimeSlotStartDate(int timeSlot)
        {
            return EpochStart.AddSeconds(timeSlot * SlotLength);
        }
    }
}