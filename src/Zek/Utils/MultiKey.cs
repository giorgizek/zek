using System;
using System.Text;

namespace Zek.Utils
{
    public class MultiKey
    {
        protected object[] KeyParts;
        private readonly int _hashCode;
        public MultiKey(params object[] keyParts)
        {
            KeyParts = keyParts ?? throw new ArgumentNullException(nameof(keyParts));
            var count = keyParts.Length;
            var hashCodes = new int[count];
            for (var i = 0; i < count; i++)
            {
                if (keyParts[i] != null)
                {
                    hashCodes[i] = keyParts[i].GetHashCode();
                }
            }
            _hashCode = HashCodeHelper.CalcHashCode(hashCodes);
        }
        public override int GetHashCode()
        {
            return _hashCode;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is MultiKey other) || GetType() != other.GetType() || KeyParts.Length != other.KeyParts.Length)
                return false;
            var count = KeyParts.Length;
            for (var i = 0; i < count; i++)
            {
                if (!Equals(KeyParts[i], other.KeyParts[i]))
                    return false;
            }
            return true;
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            var count = KeyParts.Length;
            for (var i = 0; i < count; i++)
            {
                if (KeyParts[i] != null)
                {
                    builder.Append(KeyParts[i]);
                }

                if (i < count - 1)
                    builder.Append(", ");
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
