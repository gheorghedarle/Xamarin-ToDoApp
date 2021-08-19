namespace ToDoApp.Models
{
    public class ProfileDetailsModel: BaseModel
    {
        public int TotalTasks { get; set; }
        public int DoneTasks { get; set; }
        public int TotalLists { get; set; }
    }
}
