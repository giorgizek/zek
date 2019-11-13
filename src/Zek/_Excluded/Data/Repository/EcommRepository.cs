using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Data.Repository
{



    //public interface IEcommRepository : IRepository<Ecomm>
    //{
    //    void UpdateTransactionResult(Ecomm transaction, GetTransactionResultResponseViewModel result);
    //}

    //public class EcommRepository : Repository<Ecomm>, IEcommRepository
    //{
    //    public EcommRepository(IDbContext context) : base(context)
    //    {
    //    }

    //    public void UpdateTransactionResult(Ecomm ecomm, GetTransactionResultResponseViewModel result)
    //    {
    //        if (ecomm == null)
    //            throw new ArgumentNullException(nameof(ecomm));

    //        if (result == null)
    //            throw new ArgumentNullException(nameof(result));

    //        ecomm.ResultId = result.Result;
    //        ecomm.ResultPaymentServerId = result.ResultPaymentServer;
    //        ecomm.ResultCode = result.ResultCode;
    //        ecomm.Secure3DId = result.Secure3D;
    //        ecomm.Rrn = result.Rrn;
    //        ecomm.ApprovalCode = result.ApprovalCode;
    //        ecomm.CardNumber = result.CardNumber;
    //        ecomm.Aav = result.Aav;
    //        ecomm.RegularPaymentId = result.RegularPaymentId;
    //        ecomm.RegularPaymentExpiry = result.RegularPaymentExpiry;
    //        ecomm.MerchantTransactionId = result.MerchantTransactionId;
    //        ecomm.Error = result.Error;
    //        ecomm.Response = result.Response;
    //    }
    //}
}
