using System;
using System.Globalization;
using Xamarin.Forms;


namespace ClassLibrary.Xamarin.Converters
{
    /// <summary>
    /// A converter to convert strings to lower case.
    /// </summary>
    public class StringToLowerCaseConverter : IValueConverter
    {
        /// <summary>
        /// Transforms a string to lower case.
        /// If the value is not a string, null is returned.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (value as string)?.ToLower();

        /// <summary>
        /// Not implemented => throws <see cref="NotImplementedException"/>.
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
