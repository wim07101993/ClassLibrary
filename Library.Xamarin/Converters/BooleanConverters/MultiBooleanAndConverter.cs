using System;
using System.Globalization;
using System.Linq;
using Library.Xamarin.Converters.Bases;

namespace Library.Xamarin.Converters.BooleanConverters
{
    public class MultiBooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values?.All(value => value is bool b && b) == true;
    }
}