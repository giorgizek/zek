using System;

namespace Zek.Model.Licensing
{
    [Flags]
    public enum LicenseStatus
    {
        ActivationFailed = 64,
        CryptoLicensingModuleTampered = 8388608,
        CumulativeRunTimeExceeded = 16384,
        DateRollbackDetected = 2097152,
        Deactivated = 262144,
        DebuggerDetected = 524288,
        DomainInvalid = 1048576,
        EvaluationDataLoadSaveFailed = 67108864,
        EvaluationExpired = 26500,
        EvaluationlTampered = 1024,
        ExecutionsExceeded = 512,
        Expired = 16,
        GenericFailure = 2048,
        HostAssemblyDifferent = 65536,
        InstancesExceeded = 4096,
        InValid = 134217725,
        LicenseServerMachineCodeInvalid = 33554432,
        LocalTimeInvalid = 4194304,
        MachineCodeInvalid = 8,
        NotValidated = 1,
        RemoteSessionDetected = 16777216,
        RunTimeExceeded = 8192,
        SerialCodeInvalid = 2,
        ServiceNotificationFailed = 32768,
        SignatureInvalid = 4,
        StrongNameVerificationFailed = 131072,
        UniqueUsageDaysExceeded = 256,
        UsageDaysExceeded = 128,
        UsageModeInvalid = 32,
        Valid = 0
    }
}