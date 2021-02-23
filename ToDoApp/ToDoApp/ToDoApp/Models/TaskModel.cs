namespace ToDoApp.Models
{
    public class TaskModel: BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
