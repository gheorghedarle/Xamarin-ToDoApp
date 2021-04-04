namespace ToDoApp.Models
{
    public class TaskModel: BaseModel
    {
        public string task { get; set; }
        public string projectId { get; set; }
        public string userId { get; set; }
        public bool archived { get; set; }
    }
}
