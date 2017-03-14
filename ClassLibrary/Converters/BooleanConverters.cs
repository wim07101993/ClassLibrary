using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ClassLibrary.Converters
{
    public class InvertedBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && !(bool)value;
        }
    }

    public class InvertedBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (bool)value == false)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (Visibility)value == Visibility.Collapsed)
                return true;
            return false;
        }
    }

    public class MultiBooleanAndToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                            Type targetType,
                            object parameter,
                            CultureInfo culture)
        {
            return values.Any(value => value is bool && !(bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiBooleanOrToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                            Type targetType,
                            object parameter,
                            CultureInfo culture)
        {
            return values.Any(value => value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(bool)value) return new GridLength(0);
            return parameter != null ? new GridLength((double)parameter) : new GridLength(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && ((GridLength)value).Value > 0;
        }
    }

    public class MultiBooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values,
                           Type targetType,
                           object parameter,
                           CultureInfo culture)
        {
            return values.All(value => !(value is bool) || (bool)value);
        }

        public object[] ConvertBack(object value,
                                    Type[] targetTypes,
                                    object parameter,
                                    CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
