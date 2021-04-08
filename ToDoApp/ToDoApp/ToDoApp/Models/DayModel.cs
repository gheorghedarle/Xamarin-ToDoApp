using System;

namespace ToDoApp.Models
{
    public enum DayStateEnum
    {
        Active,
        Past,
        Normal
    }

    public class DayModel : BaseModel
    {
        public DayStateEnum State { get; set; }
        public int Day { get; set; }
        public string DayName { get; set; }
        public DateTime Date { get; set; }
        public int Column { get; set; }
    }
}
