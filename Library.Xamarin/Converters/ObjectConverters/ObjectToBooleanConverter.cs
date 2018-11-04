using System;
using System.Globalization;
using Xamarin.Forms;

namespace Library.Xamarin.Converters.ObjectConverters
{
    public class ObjectToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value != null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}