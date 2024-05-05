using Microsoft.EntityFrameworkCore;
using Zek.Domain.Entities;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class PersonTitle : PocoEntity<int>
    {

    }

    public class PersonTitleMap : PocoModelMap<PersonTitle, int>
    {
        public PersonTitleMap(ModelBuilder builder) : base(builder)
        {
            ToTable("PersonTitles", "Dictionary");
        }
    }


    //public class PersonTitleTranslate : TranslateModel<PersonTitle, int>
    //{
        
    //}

    //public class PersonTitleTranslateMap : TranslateModelMap<PersonTitleTranslate, PersonTitle, int>
    //{
    //    public PersonTitleTranslateMap(ModelBuilder builder) : base(builder)
    //    {
    //        ToTable("PersonTitleTranslates", "Translate");
    //    }
    //}
}
