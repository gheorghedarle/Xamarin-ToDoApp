using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using ToDoApp.ViewModels.Templates.AddEditItem;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views.Templates.AddEditItem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditTaskTemplate : ReactiveContentView<AddEditTaskViewModel>
    {
        public AddEditTaskTemplate()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, x => x.AddTask.task, x => x.NamerErrorMessage.Text)
                    .DisposeWith(disposables);

                this.BindValidation(ViewModel, vm => vm.AddTask.task, view => view.NamerErrorMessage.Text)
                    .DisposeWith(disposables);
            });
        }
    }
}