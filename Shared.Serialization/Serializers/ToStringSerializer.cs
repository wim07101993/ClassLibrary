using System;
using System.IO;
using System.Threading.Tasks;
using Shared.Extensions;

namespace Shared.Serialization.Serializers
{
    public class ToStringSerializer : ISerializer
    {
        public string FileExtension { get; } = ".dat";


        public void Serialize(object value, TextWriter writer)
        {
            writer.Write(value.ToString());
            writer.Flush();
        }

        public string Serialize(object value)
            => value.ToString();

        public async Task SerializeAsync(object value, TextWriter writer)
        {
            writer.Write(value);
            await writer.FlushAsync();
        }

        public async Task<string> SerializeAsync(object value)
            => await new Func<object, string>(x => x.ToString()).RunAsync(value);
    }
}