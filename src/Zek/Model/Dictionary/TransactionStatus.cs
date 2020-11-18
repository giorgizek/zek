using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class TransactionStatus : BaseModel<byte>
    {
    }

    public class TransactionStatusMap : BaseModelMap<TransactionStatus, byte>
    {
        public TransactionStatusMap(ModelBuilder builder) : base(builder)
        {
            ToTable($"{nameof(TransactionStatus)}es", "Dictionary");
        }
    }



    public class TransactionStatusTranslate : TranslateModel<TransactionStatus, byte>
    {
    }

    public class TransactionStatusTranslateMap : TranslateModelMap<TransactionStatusTranslate, TransactionStatus, byte>
    {
        public TransactionStatusTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable($"{nameof(TransactionStatus)}Translates", "Translate");
        }
    }
}
