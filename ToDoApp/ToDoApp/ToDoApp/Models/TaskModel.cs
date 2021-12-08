using Plugin.CloudFirestore.Attributes;
using ToDoApp.Models.Interfaces;

namespace ToDoApp.Models
{
    public class TaskModel: DraggableItemModel
    {
        [Id]
        [MapTo("id")]
        public string Id { get; set; }
        [MapTo("archived")]
        public bool Archived { get; set; }
        [MapTo("list")]
        public string List { get; set; }
        [MapTo("task")]
        public string Task { get; set; }
        [MapTo("date")]
        public string Date { get; set; }
        [MapTo("userId")]
        public string UserId { get; set; }

        public void Update(TaskModel t)
        {
            Archived = t.Archived;
            Task = t.Task;
        }
    }
}
