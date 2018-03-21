using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public static class DateTimeExtension
    {
        public static bool CanSendOrder(this string dayDate, string lastOrderTime)
        {
            return dayDate.ToDate() > lastOrderTime.ToTime().ToShortDate().ToDate() || (dayDate == lastOrderTime.ToTime().ToShortDate() && dayDate.ToDateTime() < lastOrderTime.ToTime());
        }

        /// <summary>
        /// Convert a string into an datetime with the given time if the convestion possible
        /// </summary>
        /// <param name="time">The current time to convert</param>
        /// <returns>The time in a DateTime type.</returns>
        public static DateTime ToTime(this string time)
        {
            var today = DateTime.Now;
            if (string.IsNullOrEmpty(time) || time.Length != 4)
                return today;

            time = time.Replace(":", string.Empty);

            var strHour = time.Substring(0, 2);
            var strMinutes = time.Substring(2, 2);

            int hour, minutes;
            int.TryParse(strHour, NumberStyles.Integer, null, out hour);
            int.TryParse(strMinutes, NumberStyles.Integer, null, out minutes);

            return new DateTime(today.Year, today.Month, today.Day, hour, minutes, 0);
        }

        public static DateTime ToLongDateTime(this string date)
        {
            DateTime currentDate = DateTime.Now;
            DateTime result;
            if (!DateTime.TryParseExact(date, "yyyyMMddHHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return currentDate;

            return new DateTime(result.Year, result.Month, result.Day, currentDate.Hour, currentDate.Minute, 0);
        }

        /// <summary>
        /// Convert a string into an Datetime or the current date if the convestion isn't possible
        /// </summary>
        /// <param name="date">The current date to convert</param>
        /// <returns>The date in a DateTime type.</returns>
        public static DateTime ToDateTime(this string date)
        {
            DateTime currentDate = DateTime.Now;
            DateTime result;
            if (!DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return currentDate;

            return new DateTime(result.Year, result.Month, result.Day, currentDate.Hour, currentDate.Minute, 0);
        }

        /// <summary>
        /// Convert a string into an Datetime or the current date if the convestion isn't possible
        /// </summary>
        /// <param name="date">The current date to convert</param>
        /// <returns>The date in a DateTime type.</returns>
        public static DateTime ToDate(this string date)
        {
            DateTime result;
            if (date != null && date.IndexOf('-') == -1)
            {
                if (!DateTime.TryParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    return DateTime.Now;
            }
            else
            {
                if (!DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    return DateTime.Now;
            }

            return result;
        }

        public static string ToShortTime(this DateTime date)
        {
            return date.ToString("HHmm");
        }

        public static string ToLongTime(this DateTime date)
        {
            return date.ToString("HH:mm");
        }

        public static string ToShortDate(this DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }

        public static string ToCalendarDate(this DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

        public static string ToLongDate(this DateTime date)
        {
            return date.ToString("dd MMM yyyy");
        }

        public static string ToLongDateTime(this DateTime date)
        {
            return date.ToString("dd-MM-yyyy HH:mm");
        }

        public static string ToShortDateTime(this DateTime date)
        {
            return date.ToString("yyyyMMddHHmm");
        }

        public static int WeekNumber(this DateTime date)
        {
            var daysOffset = DayOfWeek.Monday - date.DayOfWeek;
            var mondayDate = date.AddDays(daysOffset);
            var sundayDate = mondayDate.AddDays(6);

            if (mondayDate.Year != sundayDate.Year)
                return 1;

            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(mondayDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        private static int MaxWeekNumber(this DateTime date)
        {
            var daysOffset = DayOfWeek.Monday - date.DayOfWeek;
            var mondayDate = date.AddDays(daysOffset);

            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(mondayDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// Return the year of Sunday based on the given day of the week.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int SundayYear(this DateTime date)
        {
            var daysOffset = DayOfWeek.Saturday - date.DayOfWeek + 1;
            var sundayDate = date.AddDays(daysOffset);

            return sundayDate.Year;
        }

        /// <summary>
        /// Returns the first day of the week in the currentDate.
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static DateTime StartDate(this DateTime currentDate)
        {
            var daysOffset = DayOfWeek.Monday - currentDate.DayOfWeek;

            if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                daysOffset = -6;

            return currentDate.AddDays(daysOffset);
        }


        public static string ShiftDay(this DateTime startDate, string shiftToDayOfWeek)
        {
            var daysOffset = (int)shiftToDayOfWeek.ToDate().DayOfWeek - 1;
            return startDate.AddDays(daysOffset).ToShortDate();
        }

        public static DateTime ShiftDay(this DateTime startDate, DateTime shiftToDayOfWeek)
        {
            var daysOffset = (int)shiftToDayOfWeek.DayOfWeek - 1;
            return startDate.AddDays(daysOffset);
        }

        /// <summary>
        /// Returns the first day of the week by the week number in the currentDate year.
        /// </summary>
        /// <param name="currentDate"></param>
        /// <param name="weekNumber"></param>
        /// <returns></returns>
        public static DateTime StartDate(this DateTime currentDate, int weekNumber)
        {
            return StartDate(currentDate.Year, weekNumber);
        }

        private static DateTime StartDate(int year, int weekNumber)
        {
            while (weekNumber <= 0)
            {
                var prevYearMaxWeekNumber = new DateTime(year - 1, 12, 31).MaxWeekNumber();

                weekNumber = prevYearMaxWeekNumber + weekNumber;
                year--;
            }

            // max number of weeks in current year
            var maxWeekNumber = new DateTime(year, 12, 31).MaxWeekNumber();
            while (weekNumber > maxWeekNumber)
            {
                var nextYearWeekNumber = new DateTime(year + 1, 12, 31).MaxWeekNumber();

                weekNumber = weekNumber - nextYearWeekNumber;
                year++;

                maxWeekNumber = new DateTime(year, 12, 31).MaxWeekNumber();
            }

            var firstOfJanuary = new DateTime(year, 1, 1);

            var daysOffset = DayOfWeek.Monday - firstOfJanuary.DayOfWeek;
            var firstMonday = firstOfJanuary.AddDays(daysOffset);

            if ((firstMonday.Year != year) && (weekNumber == 1))
            {
                return firstMonday;
            }

            weekNumber--;

            return firstMonday.AddDays(weekNumber * 7);
        }
    }
}
