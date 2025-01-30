using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Persistence.Configurations;

namespace Zek.Licensing
{
    public class License
    {
        public License()
        {
            _status = LicenseStatus.NotValidated;
        }

        public int Id { get; set; }

        public string? LicenseCode { get; set; }

        public string? SerialCode { get; set; }

        #region Evaluation

        public short MaxUsageDays { get; set; }
        //public bool HasMaxUsageDays { get; }

        public short MaxUniqueUsageDays { get; set; }
        //public bool HasMaxUniqueUsageDays { get; }

        public short MaxExecutions { get; set; }
        //public bool HasMaxExecutions { get; }

        public short MaxInstances { get; set; }
        //public bool HasMaxInstances { get; }

        public short MaxCumulativeRuntime { get; set; }
        //public bool HasMaxCumulativeRuntime { get; }

        public short MaxRuntime { get; set; }
        //public bool HasMaxRuntime { get; }

        public bool EnableTamperChecking { get; set; }

        #endregion

        #region Hardware Locking

        //public byte[] MachineCode { get; set; }
        public string? MachineCodeAsString { get; set; }
        public bool UseHashedMachineCode { get; set; }

        #endregion

        #region License Service

        public short MaxActivations { get; set; }
        //public bool HasMaxActivations { get; }

        public bool ActivationsAreFloating { get; set; }

        public TimeSpan FloatingLeasePeriod { get; set; }
        //public bool HasFloatingLeasePeriod { get; }

        public int FloatingHeartBeatInterval { get; set; }
        //public bool HasFloatingHeartBeatInterval { get; }



        //public byte[] LicenseServerMachineCode { get; set; }
        public string? LicenseServerMachineCodeAsString { get; set; }
        //public bool HasLicenseServerMachineCode { get; }

        public bool NotifyServiceOnValidation { get; set; }
        public bool VerifyLocalTimeWithService { get; set; }

        #endregion

        #region .Net Specific

        //public AssemblyName HostAssemblyName { get; set; }
        public string? HostAssemblyName { get; set; }
        //public bool HasHostAssemblyName { get; }

        public bool PerformHostAssemblyStrongNameVerification { get; set; }
        public bool PerformCryptoLicensingModuleStrongNameVerification { get; set; }


        public bool ValidAtRunTime { get; set; }
        public bool ValidAtDesignTime { get; set; }
        public bool HasSeparateRuntimeLicense { get; set; }
        public string? ExplicitRunTimeLicenseCode { get; set; }
        public string? AllowedDomains { get; set; }

        public bool HasAllowedDomains => !string.IsNullOrWhiteSpace(AllowedDomains);

        #endregion

        #region Miscellanous

        public string? UserData { get; set; }

        public bool HasUserData => !string.IsNullOrEmpty(UserData);

        public short NumberOfUsers { get; set; }

        public bool HasNumberOfUsers => NumberOfUsers > 0;

        public bool DetectDateRollback { get; set; }

        public bool EnableAntiDebuggerProtection { get; set; }
        public DateTime DateExpires { get; set; }
        public DateTime DateGenerated { get; set; }
        //public DateTime DateAdded { get; set; }
        public bool DisallowInRemoteSession { get; set; }

        public string? ProjectName { get; set; }
        public string? KeyName { get; set; }
        public string? ProfileName { get; set; }

        #endregion









        public string? ActivationContext { get; set; }


        public string? AssemblyStoragePath { get; set; }
        public short CurrentCumulativeRuntime { get; set; }
        public short CurrentExecutions { get; set; }
        //public short CurrentRuntime { get; }
        public short CurrentUniqueUsageDays { get; set; }
        public short CurrentUsageDays { get; set; }

        public DateTime DateLastUsed { get; set; }

        public LicenseFeatures Features { get; set; }
        public string? FileStoragePath { get; set; }


        //public bool HasDateExpires { get; }
        //public bool HasFeatures { get; }


        //public bool HasLeaseExpires { get; }

        //public bool HasMachineCode { get; }


        //public Assembly HostAssembly { get; set; }

        public DateTime LeaseExpires { get; set; }

        public string? LicenseServiceSettingsFilePath { get; set; }
        public string? LicenseServiceUrl { get; set; }

        public string? RegistryStoragePath { get; set; }
        //public short RemainingCumulativeRuntime { get; }
        //public short RemainingExecutions { get; }
        //public short RemainingRuntime { get; }
        //public short RemainingUniqueUsageDays { get; }
        //public short RemainingUsageDays { get; }
        public ServiceParamEncryptionMode ServiceParamEncryptionMode { get; set; }

        private LicenseStatus _status;

        public virtual LicenseStatus Status
        {
            get
            {
                InitStatus();
                return _status;
            }
        }

        private void InitStatus()
        {
            if (_status != LicenseStatus.NotValidated)
                return;

            CheckLicenseStatus();
        }

        internal LicenseStatus CheckLicenseStatus()
        {
            try
            {
                _status = LicenseStatus.Valid;

                if (DateTime.Now > DateExpires)
                    _status |= LicenseStatus.Expired;

                if (MaxUsageDays > 0 && DateTime.Now > DateGenerated.AddDays(MaxUsageDays))
                    _status |= LicenseStatus.UsageDaysExceeded;
            }
            catch//todo Save exception (Exception ex)
            {
                _status = LicenseStatus.GenericFailure;
            }

            return _status;
        }


        public LicenseStorageMode StorageMode { get; set; }



        public string? ValidationKey { get; set; }
    }

    //public class LicenseMap : EntityTypeMap<License>
    //{
    //    public LicenseMap(ModelBuilder builder) : base(builder)
    //    {
    //        ToTable("Licenses", "Licensing");
    //        HasKey(t => t.Id);
    //        Property(t => t.Id).ValueGeneratedOnAdd();

    //        //Property(t => t.HotelId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Hotel.T_HotelRoomType_HotelId_RoomTypeId") { IsUnique = true, Order = 1 }));
    //        //HasRequired(t => t.Hotel).WithMany().HasForeignKey(t => t.HotelId).WillCascadeOnDelete(false);

    //        //Property(t => t.RoomTypeId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Hotel.T_HotelRoomType_HotelId_RoomTypeId") { IsUnique = true, Order = 2 }));
    //        //HasRequired(t => t.RoomType).WithMany().HasForeignKey(t => t.RoomTypeId).WillCascadeOnDelete(false);



    //        Property(t => t.LicenseCode)/*.IsUnicode(false)*/.HasMaxLength(2500);
    //        Property(t => t.MachineCodeAsString)/*.IsUnicode(false)*/.HasMaxLength(200);
    //        Property(t => t.HostAssemblyName)/*.IsUnicode(false)*/.HasMaxLength(200);
    //        Property(t => t.ExplicitRunTimeLicenseCode)/*.IsUnicode(false)*/.HasMaxLength(2500);
    //        Property(t => t.AllowedDomains)/*.IsUnicode(false)*/.HasMaxLength(300);
    //        Property(t => t.UserData)/*.IsUnicode(false)*/.HasMaxLength(5000);


    //        Property(t => t.ProjectName)/*.IsUnicode(false)*/.HasMaxLength(200);
    //        Property(t => t.KeyName)/*.IsUnicode(false)*/.HasMaxLength(200);
    //        Property(t => t.ProfileName)/*.IsUnicode(false)*/.HasMaxLength(200);
    //        Property(t => t.SerialCode)/*.IsUnicode(false)*/.HasMaxLength(50);



    //        Property(t => t.DateExpires).HasColumnTypeDateTime();
    //        Property(t => t.DateGenerated).HasColumnTypeDateTime();
    //        Property(t => t.DateLastUsed).HasColumnTypeDateTime();

    //        Property(t => t.FloatingLeasePeriod).HasColumnType("time");


    //        //Property(t => t.IsDeleted).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_HotelType_IsDeleted")));

    //        //HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
    //        //Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_HotelType_CreatorId")));
    //        //Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


    //        //HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
    //        //Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_HotelType_ModifierId")));
    //        //Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
    //    }
    //}
}
