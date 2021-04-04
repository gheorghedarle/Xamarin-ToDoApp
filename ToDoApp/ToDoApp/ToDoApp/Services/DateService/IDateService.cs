using System;
using System.Collections.Generic;
using ToDoApp.Models;

namespace ToDoApp.Services.DateService
{
    public interface IDateService
    {
        WeekModel GetWeek(DateTime date);

        List<DayModel> GetDayList(DateTime firstDayInWeek, DateTime lastDayInWeek);
    }
}
