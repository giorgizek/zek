using System;

namespace Zek.Financial
{
    [Flags]
    public enum PaymentCycle
    {
        Daily = 1,

        Weekly = 2,

        Biweekly = 4,

        Semimonthly = 8,

        Monthly = 16,

        Quarterly = 32,
        
        Semiannually = 64,

        Yearly = 128,

        LumpSum = 256,
    }
}