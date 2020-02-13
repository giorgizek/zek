﻿using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Model.Base;
using Zek.Model.Contact;

namespace Zek.Model.Person
{
    public class Person<TAddress, TContact> : PersonPoco
        where TAddress : Address
        where TContact : Contact.Contact
    {
        public TAddress Address { get; set; }
        public TContact Contact { get; set; }
    }

    public class Person : Person<Address, Contact.Contact>
    {
    }

    public class PersonPoco : PocoModel
    {
        public bool IsLegal { get; set; }

        public int? TitleId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public string FullNameEn { get; set; }

        public string PersonalNumber { get; set; }
        public string Passport { get; set; }

        public DateTime? BirthDate { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }

        public int? ResidentId { get; set; }

        public int AddressId { get; set; }
        public int ContactId { get; set; }
    }

    public class PersonMap : PersonMap<Person, Address, Contact.Contact>
    {
        public PersonMap(ModelBuilder builder) : base(builder)
        {
        }
    }

    public class PersonMap<TEntity, TAddress, TContact> : PersonPocoMap<TEntity>
        where TEntity : Person<TAddress, TContact>
        where TAddress : Address
        where TContact : Contact.Contact
    {
        public PersonMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            //HasOne(p => p.Title).WithMany().HasForeignKey(p => p.TitleId).OnDelete(DeleteBehavior.Restrict);
            //HasOne(t => t.Resident).WithMany().HasForeignKey(t => t.ResidentId).OnDelete(DeleteBehavior.Restrict);
            HasOne(t => t.Address).WithMany().HasForeignKey(t => t.AddressId).OnDelete(DeleteBehavior.Restrict);
            HasOne(t => t.Contact).WithMany().HasForeignKey(t => t.ContactId).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PersonPocoMap<TEntity> : PocoModelMap<TEntity, int> where TEntity : PersonPoco
    {
        public PersonPocoMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            ToTable("Persons", "Person");

            Property(t => t.FirstName).HasMaxLength(100);
            Property(t => t.LastName).HasMaxLength(150);
            Property(t => t.FullName).HasMaxLength(255);
            Property(t => t.FirstNameEn).HasMaxLength(100);
            Property(t => t.LastNameEn).HasMaxLength(150);
            Property(t => t.FullNameEn).HasMaxLength(255);
            Property(t => t.PersonalNumber).IsRequired().HasMaxLength(50);
            Property(t => t.Passport).HasMaxLength(50);
            HasIndex(t => t.Passport);

            Property(t => t.BirthDate).HasColumnTypeDate();

            HasIndex(t => t.FullName);
            HasIndex(t => t.FullNameEn);
            HasIndex(t => t.PersonalNumber);
        }
    }

}
