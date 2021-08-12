using Plugin.CloudFirestore.Attributes;
using System;
using ToDoApp.Models.Interfaces;

namespace ToDoApp.Models
{
    public class TaskModel: DraggableItemModel
    {
        [Id]
        public string id { get; set; }
        public bool archived { get; set; }
        public string list { get; set; }
        [Ignored]
        public ListModel listObject { get; set; }
        public string task { get; set; }
        public string date { get; set; }
        [Ignored]
        public DateTime dateObject { get; set; }
        public string userId { get; set; }

        public void Update(TaskModel t)
        {
            archived = t.archived;
            task = t.task;
        }
    }
}
