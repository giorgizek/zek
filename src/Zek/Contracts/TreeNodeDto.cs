namespace Zek.Contracts
{
    public class TreeNodeDto : NodeDto
    {
        public List<TreeNodeDto> Children { get; set; } = [];
    }
}
