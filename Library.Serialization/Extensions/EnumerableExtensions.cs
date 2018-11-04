using System.Collections;
using System.Text;

namespace Library.Serialization.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToString(this IEnumerable collection,
            string delimiter = ", ",
            ISerializer serializer = null)
        {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
                return "";

            var builder = new StringBuilder();
            if (serializer == null)
                serializer = new ToStringSerializer();

            builder.Append(serializer.Serialize(enumerator.Current));

            while (enumerator.MoveNext())
            {
                builder.Append(delimiter);
                builder.Append(serializer.Serialize(enumerator.Current));
            }

            return builder.ToString();
        }
    }
}