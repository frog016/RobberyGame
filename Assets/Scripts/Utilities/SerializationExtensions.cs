using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utilities
{
    public static class SerializationExtensions
    {
        public static byte[] SerializeToByteArray(this object obj)
        {
            if (obj == null)
                return Array.Empty<byte>();

            using var memoryStream = new MemoryStream();

            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);

            return memoryStream.ToArray();
        }

        public static object DeserializeFromByteArray(this byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using var memoryStream = new MemoryStream();
            memoryStream.Write(byteArray, 0, byteArray.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            var binaryFormatter = new BinaryFormatter();
            return binaryFormatter.Deserialize(memoryStream);
        }
    }
}