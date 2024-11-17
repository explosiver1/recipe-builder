using System;
using System.Collections.Generic;

namespace RecipeBuilder.Models
{
    public static class DateHelper
    {
        public static (DateOnly startOfWeek, DateOnly endOfWeek) GetDateRangeForCurrentWeek()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek); // Assuming Sunday as the start of the week
            var endOfWeek = startOfWeek.AddDays(6);
            return (startOfWeek, endOfWeek);
        }

        public static (DateOnly startOfMonth, DateOnly endOfMonth) GetDateRangeForCurrentMonth()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var startOfMonth = new DateOnly(today.Year, today.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            var endOfMonth = new DateOnly(today.Year, today.Month, daysInMonth);
            return (startOfMonth, endOfMonth);
        }

        public static DateOnly GetStartOfWeek(DateOnly givenDate)
        {
            return givenDate.AddDays(-(int)givenDate.DayOfWeek);
        }

        public static List<DateOnly> GetDatesForWeek(DateOnly givenDate)
        {
            DateOnly startOfWeek = GetStartOfWeek(givenDate);
            var datesInWeek = new List<DateOnly>();
            for (int i = 0; i < 7; i++)
            {
                datesInWeek.Add(startOfWeek.AddDays(i));
            }
            return datesInWeek;
        }

        public static (DateOnly startOfWeek, DateOnly endOfWeek) GetDateRangeForWeek(DateOnly givenDate)
        {
            var startOfWeek = givenDate.AddDays(-(int)givenDate.DayOfWeek); // Assuming Sunday as the start of the week
            var endOfWeek = startOfWeek.AddDays(6);
            return (startOfWeek, endOfWeek);
        }
    }
}