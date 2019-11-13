using System.Text;

namespace Zek.Utils
{
    /// <summary>
    /// Equals ამოწმებს მხოლოდ Key-ს მნიშვნელობით
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class KeyPair<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public KeyPair()
        {
        }
        public KeyPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        public KeyPair<TKey, TValue> Clone()
        {
            return new KeyPair<TKey, TValue>(Key, Value);
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            if (Key != null)
            {
                builder.Append(Key);
            }
            builder.Append(", ");
            if (Value != null)
            {
                builder.Append(Value);
            }
            builder.Append(']');
            return builder.ToString();
        }
        public override bool Equals(object obj)
        {
            var pair = obj as KeyPair<TKey, TValue>;
            return pair != null && Equals(Key, pair.Key);
        }
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
