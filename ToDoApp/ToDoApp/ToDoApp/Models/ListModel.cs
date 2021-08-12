using Plugin.CloudFirestore.Attributes;

namespace ToDoApp.Models
{
    public class ListModel : BaseModel
    {
        [Id]
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        [Ignored]
        public ColorModel colorObject { get; set; }
        public string userId { get; set; }
    }
}
