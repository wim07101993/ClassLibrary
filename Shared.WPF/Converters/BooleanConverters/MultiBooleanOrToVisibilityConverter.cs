using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Shared.WPF.Converters.BooleanConverters
{
    public class MultiBooleanOrToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values?.Any(x => x is bool b && b) == true
                ? Visibility.Visible
                : Visibility.Collapsed;

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}