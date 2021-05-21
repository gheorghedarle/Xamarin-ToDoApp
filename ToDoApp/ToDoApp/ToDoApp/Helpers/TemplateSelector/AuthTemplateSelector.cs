using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Views.Templates.Auth;
using Xamarin.Forms;

namespace ToDoApp.Helpers.TemplateSelector
{
    public class AuthTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AuthLoginScreenTemplate { get; set; }
        public DataTemplate AuthSignUpScreenTemplate { get; set; }

        public AuthTemplateSelector()
        {
            AuthLoginScreenTemplate = new DataTemplate(typeof(AuthLoginScreenTemplate));
            AuthSignUpScreenTemplate = new DataTemplate(typeof(AuthSignUpScreenTemplate));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.GetType() == typeof(string))
            {
                var screen = item as string;
                if (screen == "Login")
                {
                    return AuthLoginScreenTemplate;
                }
                else
                {
                    return AuthSignUpScreenTemplate;
                }
            }
            return AuthLoginScreenTemplate;
        }
    }
}
