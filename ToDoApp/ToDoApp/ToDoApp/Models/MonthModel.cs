using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Models
{
    public class WeekModel : BaseModel
    {
        public DateTime StartDay { get; set; }
        public DateTime LastDay { get; set; }
        public string WeekString { get; set; }
    }
}
