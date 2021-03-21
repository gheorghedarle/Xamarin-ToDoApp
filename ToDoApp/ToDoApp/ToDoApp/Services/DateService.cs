using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public static class DateService
    {
        public static WeekModel GetWeek()
        {
            DayOfWeek firstDay = new CultureInfo("ro-RO").DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = DateTime.Now.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            var lastDayInWeek = firstDayInWeek.AddDays(6);
            return new WeekModel()
            {
                StartDay = firstDayInWeek,
                LastDay = lastDayInWeek,
                WeekString = $"{firstDayInWeek.ToString("MMMM")} {firstDayInWeek.Day}-{lastDayInWeek.Day}"
            };
        }

        public static List<DayModel> GetDayList(DateTime firstDayInWeek, DateTime lastDayInWeek)
        {
            List<DayModel> dayList = new List<DayModel>();
            for (var i = 0; i < 7; i++)
            {
                var date = firstDayInWeek.AddDays(i);
                dayList.Add(new DayModel()
                {
                    Day = date.Day,
                    DayName = date.ToString("ddd"),
                    IsActive = date.Date == DateTime.Now.Date,
                    Column = i
                });
            }
            return dayList;
        }
    }
}
