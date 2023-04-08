using System.Text;

namespace Zek.Utils
{
    /// <summary>
    /// Equals checked only Id property
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class IdHash<TId>
    {
        public TId Id { get; set; }

        public string Hash { get; set; }

        public IdHash()
        {
        }
        public IdHash(TId key)
        {
            Id = key;
        }
        public IdHash(TId key, string hash)
        {
            Id = key;
            Hash = hash;
        }
        public IdHash<TId> Clone()
        {
            return new(Id, Hash);
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            if (Id != null)
            {
                builder.Append(Id);
            }
            builder.Append(", ");
            if (Hash != null)
            {
                builder.Append(Hash);
            }
            builder.Append(']');
            return builder.ToString();
        }
        public override bool Equals(object obj)
        {
            return obj is IdHash<TId> pair && Equals(Id, pair.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }


    public class IdHash : IdHash<int>
    {
        public IdHash()
        {
        }
        public IdHash(int id) : base(id)
        {
        }
        public IdHash(int id, string hash) : base(id, hash)
        {
        }
    }
}
