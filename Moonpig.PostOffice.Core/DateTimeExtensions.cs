using System;
using System.Collections.Generic;
using System.Text;

namespace Moonpig.PostOffice.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWorkingDays(this DateTime date, int days)
        {
            var updatedDate = date.AddDays(days);
            if (updatedDate.DayOfWeek == DayOfWeek.Saturday)
                return updatedDate.AddDays(2);
            if (updatedDate.DayOfWeek == DayOfWeek.Sunday)
                return updatedDate.AddDays(1);
            return updatedDate;
        }
    }
}
