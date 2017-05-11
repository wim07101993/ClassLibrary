using System;
using System.Globalization;


namespace XamarinClassLibrary.Converters.Bases
{
    public interface IMultiValueConverter
    {
        object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
    }
}
