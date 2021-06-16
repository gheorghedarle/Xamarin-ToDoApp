using Plugin.CloudFirestore.Attributes;
using ToDoApp.Models.Interfaces;

namespace ToDoApp.Models
{
    public class TaskModel: DraggableItemModel
    {
        [Id]
        public string id { get; set; }
        public bool archived { get; set; }
        public string projectId { get; set; }
        public string projectName { get; set; }
        public string task { get; set; }
        public string userId { get; set; }
    }
}
