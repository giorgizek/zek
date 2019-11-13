using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;
using Zek.Model.Identity;

namespace Zek.Model.Dictionary
{

    public class PmType<TUser> : BaseModel<int, TUser>
          where TUser : User
    {

    }

    public class PmTypeMap<TUser> : BaseModelMap<PmType<TUser>, int, TUser>
          where TUser : User
    {
        public PmTypeMap(ModelBuilder builder) : base(builder)
        {
            ToTable("PmTypes", "Dictionary");
        }
    }



    public class PmTypeTranslate<TUser> : TranslateModel<PmType<TUser>, int>
        where TUser : User
    {

    }

    public class PmTypeTranslateMap<TUser> : TranslateModelMap<PmTypeTranslate<TUser>, PmType<TUser>, int>
         where TUser : User
    {
        public PmTypeTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("PmTypeTranslates", "Translate");
        }
    }
}
