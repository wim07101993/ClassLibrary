﻿using System;
using System.Globalization;
using System.Windows.Data;


namespace WindowsClassLibrary.Converters
{
    /// <summary>
    /// A converter to check if a value not is null.
    /// </summary>
    public class ObjectToBooleanConverter : IValueConverter
    {
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

    /// <summary>
    /// A converter to check if a value is null
    /// </summary>
    public class InvertedObjectToBooleanConverter : IValueConverter
    {
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
