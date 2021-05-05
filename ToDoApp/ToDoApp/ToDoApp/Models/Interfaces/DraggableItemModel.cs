namespace ToDoApp.Models.Interfaces
{
    public class DraggableItemModel: BaseModel
    {
        public bool isBeingDragged { get; set; }
        public bool isBeingDraggedOver { get; set; }
    }
}
