using System;
using System.Globalization;
using System.Windows.Data;

namespace Shared.WPF.Converters
{
    public class NotZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => double.TryParse((value ?? "").ToString(), out var val)
                ? (object) (Math.Abs(val) > 0.0)
                : null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
