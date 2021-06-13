using ToDoApp.Views.Templates.AddItem;
using Xamarin.Forms;

namespace ToDoApp.Helpers.TemplateSelector
{
    public class AddItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AddTaskTemplate { get; set; }
        public DataTemplate AddListTemplate { get; set; }

        public AddItemTemplateSelector()
        {
            AddTaskTemplate = new DataTemplate(typeof(AddTaskTemplate));
            AddListTemplate = new DataTemplate(typeof(AddListTemplate));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.GetType() == typeof(string))
            {
                var screen = item as string;
                if (screen == "task")
                {
                    return AddTaskTemplate;
                }
                else
                {
                    return AddListTemplate;
                }
            }
            return AddTaskTemplate;
        }
    }
}
