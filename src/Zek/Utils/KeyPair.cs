using System.Text;

namespace Zek.Utils
{
    /// <summary>
    /// Equals checked only Key property
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
            return new(Key, Value);
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
            return obj is KeyPair<TKey, TValue> pair && Equals(Key, pair.Key);
        }
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }



    public class KeyPair : KeyPair<int, string>
    {
        public KeyPair()
        {
        }
        public KeyPair(int key, string value)
        {
            Key = key;
            Value = value;
        }
    }


    public class KeyPairCode : KeyPair
    {
        public string Code { get; set; }
    }
}
