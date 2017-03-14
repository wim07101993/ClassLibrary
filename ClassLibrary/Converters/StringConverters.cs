﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace ClassLibrary.Converters
{
    public class StringToLowerCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value)?.ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
