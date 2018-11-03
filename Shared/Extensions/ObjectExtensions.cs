using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Shared.Extensions
{
    public static class ObjectExtensions
    {
        #region CONVERSION
        
        public static double ToDouble(this object value)
        {
            var d = value as double? ?? double.NaN;
            return double.IsInfinity(d)
                ? double.NaN
                : d;
        }

        #endregion CONVERSION
    }
}