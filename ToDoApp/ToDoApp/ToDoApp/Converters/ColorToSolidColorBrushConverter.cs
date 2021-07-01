using System;
using System.Globalization;
using Xamarin.Forms;

namespace ToDoApp.Converters
{
    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if(value is Color)
            {
                var color = (Color)value;
                return new SolidColorBrush(color);
            }
            else
            {
                var hex = (string)value;
                var color = Color.FromHex(hex);
                return new SolidColorBrush(color);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
