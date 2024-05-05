using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Zek.Domain.Entities;
using Zek.Model.Base;

namespace Zek.Model.Form
{
    public class Form : PocoEntity
    {
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
    }

    public class FormMap : PocoModelMap<Form>
    {
        public FormMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Document", "Forms");
        }
    }
}