using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public static class DateService
    {
        public static List<MonthModel> GetMonthList()
        {
            List<MonthModel> monthList = new List<MonthModel>();
            foreach (var month in DateTimeFormatInfo.CurrentInfo.MonthNames)
            {
                monthList.Add(new MonthModel()
                {
                    Name = month,
                    IsActive = false
                });
            }
            monthList[DateTime.Now.Month - 1].IsActive = true;
            return monthList;
        }

        public static List<DayModel> GetDayList()
        {
            List<DayModel> dayList = new List<DayModel>();
            var firstDayOfTheCurrentWeek = DateTime.Now.AddDays(((int)DateTime.Now.DayOfWeek - 1) * -1);
            for (var i = 0; i < 7; i++)
            {
                var date = firstDayOfTheCurrentWeek.AddDays(i);
                dayList.Add(new DayModel()
                {
                    Day = date.Day,
                    DayName = date.ToString("ddd"),
                    IsActive = date.Date == DateTime.Now.Date,
                    Column = i
                });
            }
            return dayList.Take(7).ToList();
        }
    }
}
