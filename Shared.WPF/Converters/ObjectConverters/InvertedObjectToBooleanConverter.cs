using System;
using System.Globalization;
using System.Windows.Data;

namespace Shared.WPF.Converters.ObjectConverters
{
    public class InvertedObjectToBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Returns true when a value is null.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value == null;

        /// <inheritdoc />
        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
