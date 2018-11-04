﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Library.Extensions;

namespace Library.WPF.Converters
{
    public class ArcEndPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var actualWidth = values[0].ToDouble();
            var value = values[1].ToDouble();
            var minimum = values[2].ToDouble();
            var maximum = values[3].ToDouble();

            if (new[] {actualWidth, value, minimum, maximum}.Any(double.IsNaN))
                return Binding.DoNothing;

            if (values.Length == 5)
            {
                var fullIndeterminateScaling = values[4].ToDouble();
                if (!double.IsNaN(fullIndeterminateScaling) && fullIndeterminateScaling > 0.0)
                    value = (maximum - minimum) * fullIndeterminateScaling;
            }

            var percent = maximum <= minimum
                ? 1.0
                : (value - minimum) / (maximum - minimum);
            var degrees = 360 * percent;
            var radians = degrees * (Math.PI / 180);

            var centre = new Point(actualWidth / 2, actualWidth / 2);
            var hypotenuseRadius = actualWidth / 2;

            var adjacent = Math.Cos(radians) * hypotenuseRadius;
            var opposite = Math.Sin(radians) * hypotenuseRadius;

            return new Point(centre.X + opposite, centre.Y - adjacent);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}