using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Library.WPF.Converters
{
    public class StartPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is double d && d > 0.0
                ? new Point(d / 2, 0)
                : new Point();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}