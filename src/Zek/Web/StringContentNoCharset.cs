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
            if (Headers.ContentType is not null)
                Headers.ContentType.CharSet = null;
        }

        public StringContentNoCharset(string content, Encoding encoding, string mediaType) : base(content, encoding, mediaType)
        {
            if (Headers.ContentType is not null)
                Headers.ContentType.CharSet = null;
        }

        public StringContentNoCharset(string content, string mediaType) : base(content, Encoding.UTF8, mediaType)
        {
            if (Headers.ContentType is not null)
                Headers.ContentType.CharSet = null;
        }

    }
}
