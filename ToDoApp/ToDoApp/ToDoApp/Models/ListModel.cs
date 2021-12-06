using Plugin.CloudFirestore.Attributes;

namespace ToDoApp.Models
{
    public class ListModel : BaseModel
    {
        [Id]
        [MapTo("id")]
        public string Id { get; set; }
        [MapTo("name")]
        public string Name { get; set; }
        [MapTo("color")]
        public string Color { get; set; }
        [Ignored]
        public ColorModel colorObject { get; set; }
        [MapTo("userId")]
        public string UserId { get; set; }
    }
}
