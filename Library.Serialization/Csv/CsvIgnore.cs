using System;

namespace Shared.Serialization.Csv
{
    /// <summary>
    /// Instructs the <see cref="CsvSerializer"/> not to serialize the public field or public read/write property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class CsvIgnore : Attribute
    {
        
    }
}