namespace Zek.Licensing
{
    [Flags]
    public enum LicenseStatus
    {
        Valid = 0,
        NotValidated = 1,
        SerialCodeInvalid = 2,
        SignatureInvalid = 4,
        MachineCodeInvalid = 8,
        Expired = 16,
        UsageModeInvalid = 32,
        ActivationFailed = 64,
        UsageDaysExceeded = 128,
        UniqueUsageDaysExceeded = 256,
        ExecutionsExceeded = 512,
        EvaluationlTampered = 1024,
        GenericFailure = 2048,
        InstancesExceeded = 4096,
        RunTimeExceeded = 8192,
        CumulativeRunTimeExceeded = 16384,
        ServiceNotificationFailed = 32768,
        HostAssemblyDifferent = 65536,
        StrongNameVerificationFailed = 131072,
        Deactivated = 262144,
        DebuggerDetected = 524288,
        DomainInvalid = 1048576,
        DateRollbackDetected = 2097152,
        LocalTimeInvalid = 4194304,
        CryptoLicensingModuleTampered = 8388608,
        RemoteSessionDetected = 16777216,
        LicenseServerMachineCodeInvalid = 33554432,
        EvaluationDataLoadSaveFailed = 67108864,
        InValid = 134217725,
        EvaluationExpired = 26500,
    }
}