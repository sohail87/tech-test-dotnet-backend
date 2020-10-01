using System;
using System.Collections.Generic;
using System.Text;

namespace Moonpig.PostOffice.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWorkingDays(this DateTime date, int days)
        {
            if (date.DayOfWeek == DayOfWeek.Friday && days == 2) return date.AddDays(4);
            var updatedDate = date.AddDays(days);
            if (updatedDate.DayOfWeek == DayOfWeek.Saturday)
                updatedDate = updatedDate.AddDays(2);
            if (updatedDate.DayOfWeek == DayOfWeek.Sunday)
                updatedDate = updatedDate.AddDays(1);
            return updatedDate;
        }
    }
}
