using System;
using System.Globalization;

namespace Library.Xamarin.Converters.Bases
{
    public interface IMultiValueConverter
    {
        object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
    }
}
