﻿using System;

namespace Moonpig.PostOffice.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWorkingDays(this DateTime dateTime, int days)
        {
            if (dateTime.IsWeekend()) days++;

            while (days != 0)
            {
                dateTime = dateTime.AddDays(1);

                if (dateTime.IsWeekday())
                    days--;
            }

            return dateTime;
        }

        private static bool IsWeekday(this DateTime dateTime) 
            => !IsWeekend(dateTime);

        private static bool IsWeekend(this DateTime dateTime) 
            => dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    }
}
