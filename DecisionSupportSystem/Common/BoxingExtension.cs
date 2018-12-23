using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DecisionSupportSystem.Common
{
    static class BoxingExtension
    {
        private static readonly BinaryFormatter BinaryFormatter;

        static BoxingExtension()
        {
            BinaryFormatter = new BinaryFormatter();
        }

        public static byte[] Boxing(object item)
        {
            using (var serializationStream = new MemoryStream())
            {
                BinaryFormatter.Serialize(serializationStream, item);
                return serializationStream.ToArray();
            }
        }
        public static object Unboxing(byte[] item)
        {
            using (var deserializationStream = new MemoryStream())
            {
                deserializationStream.Write(item, 0, item.Length);
                deserializationStream.Position = 0;
                return BinaryFormatter.Deserialize(deserializationStream);
            }
        }
    }
}
