using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Shared.WPF.Converters.BooleanConverters
{
    public class MultiBooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values?.All(x => x is bool b && b) == true;

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) 
            => throw new NotImplementedException();
    }
}