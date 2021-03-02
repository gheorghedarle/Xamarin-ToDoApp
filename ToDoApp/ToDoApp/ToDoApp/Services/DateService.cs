using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public static class DateService
    {
        public static List<MonthModel> GetMonthList()
        {
            List<MonthModel> monthList = new List<MonthModel>();
            foreach(var month in DateTimeFormatInfo.CurrentInfo.MonthNames)
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
    }
}
