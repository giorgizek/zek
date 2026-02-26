namespace Zek.Contracts
{
    public class TreeNodeDto : NodeDto
    {
        public List<TreeNodeDto> Children { get; set; } = [];
    }

    public class TreeNodeDto<TId> : NodeDto<TId>
    {
        public List<TreeNodeDto> Children { get; set; } = [];
    }
}
