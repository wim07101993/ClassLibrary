using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;


namespace ClassLibrary.Windows.Converters
{
    /// <summary>
    /// A converter to invert booleans.
    /// </summary>
    public class InvertedBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Returns true when the value not is a bool and the value is false.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool && !(bool) value;

        /// <summary>
        /// Returns true when the value not is a bool and the value is false.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool && !(bool) value;
    }

    /// <summary>
    /// A converter to convert a boolean to visibility.
    /// </summary>
    public class InvertedBooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Returns <see cref="Visibility.Visible"/> when the value is a bool and the value iss false.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool && !(bool) value
                ? Visibility.Visible
                : Visibility.Collapsed;

        /// <summary>
        /// Returns true when the value is <see cref="Visibility.Collapsed"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Visibility && (Visibility) value == Visibility.Collapsed;
    }

    /// <summary>
    /// A multiconverter to convert multiple booleans to visibility with the AND relation.
    /// </summary>
    public class MultiBooleanAndToVisibilityConverter : IMultiValueConverter
    {
        /// <summary>
        /// returns <see cref="Visibility.Visible"/> if all elements in <see cref="values"/> are true.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values.Any(value => value is bool && !(bool) value)
                ? Visibility.Collapsed
                : Visibility.Visible;

        /// <summary>
        /// Not implemented => throws <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    /// <summary>
    /// A multiconverter to convert multiple booleans to visibility with the OR relation.
    /// </summary>
    public class MultiBooleanOrToVisibilityConverter : IMultiValueConverter
    {
        /// <summary>
        /// Returns <see cref="Visibility.Visible"/> if any of all elements in <see cref="values"/> is true.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values.Any(value => value is bool && (bool) value)
                ? Visibility.Visible
                : Visibility.Collapsed;

        /// <summary>
        /// Not implemented => throws <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    /// <summary>
    /// A converter to convert a boolean to a GridLength.
    /// </summary>
    public class BooleanToGridLengthConverter : IValueConverter
    {
        /// <summary>
        /// Returns the parameter converted to a GridLength if the value is true.
        /// Else a GridLength with value 0.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool) || !(bool) value)
                return new GridLength(0);

            return parameter is double
                ? new GridLength((double) parameter)
                : new GridLength(0);
        }

        /// <summary>
        /// Returns true if the value is a GridLength and its value is greater than 0
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is GridLength && ((GridLength) value).Value > 0;
    }

    /// <summary>
    /// A converter to convert multiple booleans to one with an AND relation.
    /// </summary>
    public class MultiBooleanAndConverter : IMultiValueConverter
    {
        /// <summary>
        /// Returns true if all booleans in the values are true.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values.All(value => value is bool && (bool) value);

        /// <summary>
        /// Not implemented => throws <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) 
            => throw new NotImplementedException();
    }
}