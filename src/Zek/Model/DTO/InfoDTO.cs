using Zek.Utils;

namespace Zek.Model.DTO
{
    [Obsolete]
    public class InfoDTO
    {
        public int? Id { get; set; }

        public bool IsDeleted { get; set; }

        public KeyPair<int?, string>? Creator { get; set; }

        public DateTime? CreateDate { get; set; }

        public KeyPair<int?, string>? Modifier { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public KeyPair<int?, string>? Approver { get; set; }

        public DateTime? ApproveDate { get; set; }
    }
}
