using System.Text;

namespace Zek.Utils
{
    public class Pair<TFirst, TSecond>
    {
        public TFirst First { get; set; } = default!;
        public TSecond Second { get; set; } = default!;

        public Pair()
        {
        }
        public Pair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }

        public Pair<TFirst, TSecond> Clone()
        {
            return new(First, Second);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            if (First != null)
            {
                builder.Append(First);
            }
            builder.Append(", ");
            if (Second != null)
            {
                builder.Append(Second);
            }
            builder.Append(']');
            return builder.ToString();
        }

        public override bool Equals(object? obj)
        {
            var pair = obj as Pair<TFirst, TSecond>;
            if (pair == null)
                return false;
            return Equals(First, pair.First) && Equals(Second, pair.Second);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Pair<TFirst, TSecond, TThird> : Pair<TFirst, TSecond>
    {
        public Pair() { }
        public Pair(TFirst first, TSecond second, TThird third)
            : base(first, second)
        {
            Third = third;
        }

        public TThird Third { get; set; } = default!;

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            if (First != null)
            {
                builder.Append(First);
            }
            builder.Append(", ");
            if (Second != null)
            {
                builder.Append(Second);
            }
            builder.Append(", ");
            if (Third != null)
            {
                builder.Append(Third);
            }
            builder.Append(']');
            return builder.ToString();
        }



        public override bool Equals(object? obj)
        {
            var pair = obj as Pair<TFirst, TSecond, TThird>;
            if (pair == null)
                return false;
            return Equals(First, pair.First) && Equals(Second, pair.Second) && Equals(Third, pair.Third);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Pair<TFirst, TSecond, TThird, TFourth> : Pair<TFirst, TSecond, TThird>
    {
        public Pair() { }
        public Pair(TFirst first, TSecond second, TThird third, TFourth fourth)
            : base(first, second, third)
        {
            Fourth = fourth;
        }

        public TFourth Fourth { get; set; } = default!;

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            if (First != null)
            {
                builder.Append(First);
            }
            builder.Append(", ");
            if (Second != null)
            {
                builder.Append(Second);
            }
            builder.Append(", ");
            if (Third != null)
            {
                builder.Append(Third);
            }
            builder.Append(", ");
            if (Fourth != null)
            {
                builder.Append(Fourth);
            }
            builder.Append(']');
            return builder.ToString();
        }



        public override bool Equals(object? obj)
        {
            var pair = obj as Pair<TFirst, TSecond, TThird, TFourth>;
            if (pair == null)
                return false;
            return Equals(First, pair.First) && Equals(Second, pair.Second) && Equals(Third, pair.Third) && Equals(Fourth, pair.Fourth);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
