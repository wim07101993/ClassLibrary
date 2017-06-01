using System;
using System.Globalization;
using System.Windows.Data;


namespace ClassLibrary.Windows.Converters
{
    /// <summary>
    /// A converter to convert 
    /// </summary>
    public class DateTimeToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
                return value;

            return ((DateTime)value).ToLongDateString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                throw new ArgumentException();

            return DateTime.Parse((string)value);
        }
    }
}
