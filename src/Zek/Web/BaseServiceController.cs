using System;

namespace Zek.Web
{
    public class BaseServiceController<TService> : BaseController
        where TService : IDisposable
    {
        public BaseServiceController(TService service)
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

    public class BaseServiceController<TService1, TService2> : BaseController
        where TService1 : IDisposable
        where TService2 : IDisposable
    {
        public BaseServiceController(TService1 service1, TService2 service2)
        {
            Service1 = service1;
            Service2 = service2;
        }
        protected TService1 Service1 { get; set; }
        protected TService2 Service2 { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Service1?.Dispose();
                Service2?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
