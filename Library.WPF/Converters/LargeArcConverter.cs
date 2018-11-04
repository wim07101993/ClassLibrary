using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Library.Extensions;

namespace Library.WPF.Converters
{
    public class LargeArcConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = values[0].ToDouble();
            var minimum = values[1].ToDouble();
            var maximum = values[2].ToDouble();

            if (new[] {value, minimum, maximum}.Any(double.IsNaN))
                return Binding.DoNothing;

            if (values.Length == 4)
            {
                var fullIndeterminateScaling = values[3].ToDouble();
                if (!double.IsNaN(fullIndeterminateScaling) && fullIndeterminateScaling > 0.0)
                    value = (maximum - minimum) * fullIndeterminateScaling;
            }

            var percent = maximum > minimum
                ? (value - minimum) / (maximum - minimum)
                : 1.0;

            return percent > 0.5;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}