using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace ClassLibrary.Windows.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// A converter to check if a value not is null.
    /// </summary>
    public class ObjectToBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Returns true when the value not is  null.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => value != null;

        /// <inheritdoc />
        /// <summary>
        /// Not implemented =&gt; throws <see cref="T:System.NotImplementedException" />.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    /// <inheritdoc />
    /// <summary>
    /// A converter to check if a value is null
    /// </summary>
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
        /// Not implemented =&gt; throws <see cref="T:System.NotImplementedException" />.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
            => throw new NotImplementedException();
    }

    /// <inheritdoc />
    /// <summary>
    /// A converter to convert an object to a string
    /// </summary>
    public class ObjectToStringConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Converts the <see cref="!:value" /> to a <see cref="T:System.String" /> with the .ToString() method.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString();

        /// <inheritdoc />
        /// <summary>
        /// Not implemented =&gt; throws <see cref="T:System.NotImplementedException" />.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
            => throw new NotImplementedException();
    }

    /// <inheritdoc />
    /// <summary>
    /// A converter set the visibility of something to Collapsed when the value is null.
    /// </summary>
    public class ObjectToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Returns Visibility.Visible when the value not is  null, else Visibility.Collapsed.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        /// <inheritdoc />
        /// <summary>
        /// Not implemented =&gt; throws <see cref="T:System.NotImplementedException" />.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
