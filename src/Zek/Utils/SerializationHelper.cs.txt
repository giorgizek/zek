using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Zek.Utils
{
    public class SerializationHelper
    {
        public static string SerializeXmlString(object instance, bool includeNamespaces = false)
        {
            if (instance == null) return null;

            var buffer = includeNamespaces ? SerializeXml(instance) : SerializeXmlWithoutNamespaces(instance);
            return Encoding.UTF8.GetString(buffer);
        }
        public static byte[] SerializeXml(object instance)
        {
            if (instance == null) return null;

            using (var ms = new MemoryStream())
            {

                var settings = new XmlWriterSettings
                {
                    Encoding = new UTF8Encoding(false),
                    Indent = true,
                    IndentChars = "\t",
                };

                var xmlSerializer = new XmlSerializer(instance.GetType());
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    xmlSerializer.Serialize(writer, instance);
                }
                return ms.ToArray();
            }
        }
        public static byte[] SerializeXmlWithoutNamespaces(object instance)
        {
            if (instance == null) return null;

            using (var ms = new MemoryStream())
            {
                var xsn = new XmlSerializerNamespaces();
                xsn.Add(string.Empty, string.Empty);

                var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    Encoding = new UTF8Encoding(false),
                };

                var xmlSerializer = new XmlSerializer(instance.GetType());
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    xmlSerializer.Serialize(writer, instance, xsn);
                }
                return ms.ToArray();
            }
        }
    }
}
