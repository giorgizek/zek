using Zek.Utils;

namespace Zek.Model.DTO
{
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
}