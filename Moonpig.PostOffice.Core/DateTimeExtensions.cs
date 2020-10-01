using System;
using System.Collections.Generic;
using System.Text;

namespace Moonpig.PostOffice.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWorkingDays(this DateTime dateTime, int days)
        {
            while (days != 0)
            {
                dateTime = dateTime.AddDays(1);

                if (dateTime.IsWeekday())
                    days -= 1;
            }

            return dateTime;
        }

        private static bool IsWeekday(this DateTime dateTime) 
            => !IsWeekend(dateTime);

        private static bool IsWeekend(this DateTime dateTime) 
            => dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    }
}
