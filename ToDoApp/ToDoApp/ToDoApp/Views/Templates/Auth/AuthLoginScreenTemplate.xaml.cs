using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoApp.Views.Templates.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthLoginScreenTemplate : StackLayout
    {
        public AuthLoginScreenTemplate()
        {
            InitializeComponent();
        }
    }
}