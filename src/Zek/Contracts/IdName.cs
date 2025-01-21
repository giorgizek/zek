namespace Zek.Contracts
{
    public class IdName : IdName<int, string>
    {
    }

    public class IdName<TId, TName>
    {
        public TId? Id { get; set; }

        public TName? Name { get; set; }
    }
}
