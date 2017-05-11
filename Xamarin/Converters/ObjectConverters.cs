﻿using System;
using System.Globalization;
using Xamarin.Forms;


namespace ClassLibrary.Xamarin.Converters
{
    /// <summary>
    /// A converter to convert an object to a string
    /// </summary>
    public class ObjectToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts the <see cref="value"/> to a <see cref="string"/> with the .ToString() method.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.ToString();

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