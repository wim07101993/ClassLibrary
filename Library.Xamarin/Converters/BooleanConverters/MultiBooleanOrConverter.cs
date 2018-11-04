using System;
using System.Globalization;
using System.Linq;
using Shared.Xamarin.Converters.Bases;

namespace Shared.Xamarin.Converters.BooleanConverters
{
    public class MultiBooleanOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values?.Any(x => x is bool b && b) == true;
    }
}