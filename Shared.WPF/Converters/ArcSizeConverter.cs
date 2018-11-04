using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Shared.WPF.Converters
{
    public class ArcSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is double d && d > 0.0
                ? (object) new Size(d / 2, d / 2)
                : new Point();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
