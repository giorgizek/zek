namespace Zek.Utils
{
    public class IdLink<T> : IdLinkBase
    {
        public IdLink(T value)
        {
            this.Value = value;
        }
        public IdLink()
        {

        }
        public T Value { get; set; } = default!;
    }
}