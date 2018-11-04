using System;
using System.Globalization;
using System.Linq;
using ClassLibrary.Xamarin.Converters.Bases;

namespace ClassLibrary.Xamarin.Converters.BooleanConverters
{
    public class MultiBooleanAndConverter : IMultiValueConverter
    {
       public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values.All(value => value is bool && (bool)value);

       public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
