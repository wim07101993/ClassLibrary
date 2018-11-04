using System;
using System.Globalization;
using System.Windows.Data;

namespace Library.WPF.Converters
{
    public class RotateTransformCentreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            //value == actual width
            => (double?) value / 2 ?? 0;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}