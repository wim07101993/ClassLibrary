﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Shared.WPF.Converters
{
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thickness = (Thickness?) value;
            return thickness?.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;
    }
}