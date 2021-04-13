using System;

namespace ToDoApp.Models
{
    public enum DayStateEnum
    {
        Active,
        Normal,
        Past,
    }

    public class DayModel : BaseModel
    {
        public int Column { get; set; }
        public DateTime Date { get; set; }
        public int Day { get; set; }
        public string DayName { get; set; }
        public DayStateEnum State { get; set; }
    }
}
