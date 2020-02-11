using System;
using Microsoft.AspNetCore.Mvc;

namespace Zek.Web
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class ApiControllerBase<TService> : ApiControllerBase
        where TService : IDisposable
    {
        public ApiControllerBase(TService service)
        {
            Service = service;
        }
        protected TService Service { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Service?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    //public class ApiControllerBase<TService1, TService2> : ApiControllerBase
    //    where TService1 : IDisposable
    //    where TService2 : IDisposable
    //{
    //    public ApiControllerBase(TService1 service1, TService2 service2)
    //    {
    //        Service1 = service1;
    //        Service2 = service2;
    //    }
    //    protected TService1 Service1 { get; set; }
    //    protected TService2 Service2 { get; set; }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            Service1?.Dispose();
    //            Service2?.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //}
}