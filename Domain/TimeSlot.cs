using System;

namespace DevChallenge.Domain
{
    /// <summary>
    /// TimeSlot 
    /// </summary>
    public class TimeSlot
    {
        /// <summary>
        /// Start DateTime of first slot.
        /// </summary>
        public static readonly DateTime EpochStart = new DateTime(2018,01,01,0,0,0);
        /// <summary>
        /// SlotLength - length of one time slot in seconds
        /// </summary>
        public const int SlotLength = 10000;
    }
}