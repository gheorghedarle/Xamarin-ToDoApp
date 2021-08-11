using Xamarin.Forms;

namespace ToDoApp.Views.Templates.AddItem
{
    public class BaseAddItemTemplate : ContentView
    {
        public static BindableProperty ParentContextProperty = BindableProperty.Create(nameof(ParentBindingContext), typeof(object), typeof(BaseAddItemTemplate), null);

        public object ParentBindingContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }
    }
}
