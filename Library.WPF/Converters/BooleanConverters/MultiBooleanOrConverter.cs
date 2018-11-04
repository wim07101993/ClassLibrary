using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Library.WPF.Converters.BooleanConverters
{
    public class MultiBooleanOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values?.Any(x => x is bool b && b) == true;

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}