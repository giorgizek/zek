using Microsoft.EntityFrameworkCore;
using Zek.Domain.Entities;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class AccountType : PocoEntity<int>
    {
        public List<AccountType> Children { get; set; }

        public int? ParentId { get; set; }
        public AccountType Parent { get; set; }
    }

    public class AccountTypeMap : PocoModelMap<AccountType, int>
    {
        public AccountTypeMap(ModelBuilder builder) : base(builder)
        {
            ToTable("AccountTypes", "Dictionary");
            HasOne(t => t.Parent).WithMany(t => t.Children).HasForeignKey(t => t.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
    }



    //public class AccountTypeTranslate : TranslateModel<AccountType, int>
    //{
    //}

    //public class AccountTypeTranslateMap : TranslateModelMap<AccountTypeTranslate, AccountType, int>
    //{
    //    public AccountTypeTranslateMap(ModelBuilder builder) : base(builder)
    //    {
    //        ToTable("AccountTypeTranslates", "Translate");
    //    }
    //}
}
