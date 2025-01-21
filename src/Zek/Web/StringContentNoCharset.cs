using System.Text;

namespace Zek.Web
{
    public class StringContentNoCharset : StringContent
    {
        public StringContentNoCharset(string content) : base(content)
        {
        }

        public StringContentNoCharset(string content, Encoding encoding) : base(content, encoding)
        {
            Headers.ContentType.CharSet = null;
        }

        public StringContentNoCharset(string content, Encoding encoding, string mediaType) : base(content, encoding, mediaType)
        {
            Headers.ContentType.CharSet = null;
        }

        public StringContentNoCharset(string content, string mediaType) : base(content, Encoding.UTF8, mediaType)
        {
            Headers.ContentType.CharSet = null;
        }

    }
}
