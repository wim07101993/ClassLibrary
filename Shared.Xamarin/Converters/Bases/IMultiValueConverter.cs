using System;
using System.Globalization;


namespace ClassLibrary.Xamarin.Converters.Bases
{
    public interface IMultiValueConverter
    {
        object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
    }
}
