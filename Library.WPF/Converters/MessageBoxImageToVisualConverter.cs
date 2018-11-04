using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Library.WPF.Converters
{
    public class MessageBoxImageToVisualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is MessageBoxImage icon))
                return null;

            string name;
            switch (icon)
            {
                case MessageBoxImage.None:
                    return null;
                case MessageBoxImage.Error:
                    name = "IconError";
                    break;
                case MessageBoxImage.Warning:
                    name = "IconWarning";
                    break;
                case MessageBoxImage.Information:
                    name = "IconInfo";
                    break;
                case MessageBoxImage.Question:
                    name = "IconQuestion";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Application.Current.TryFindResource(name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}