using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoApp.Models
{
    public class ProjectModel : BaseModel
    {
        public string name { get; set; }
        public string projectId { get; set; }
        public string userId { get; set; }
    }
}
