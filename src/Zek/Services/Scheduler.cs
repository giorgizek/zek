namespace Zek.Services
{
    //public class SchedulerOptions
    //{

    //}
    //public class Scheduler
    //{
    //    private Timer _timer;
    //    private SchedulerOptions _options;

    //    public Scheduler(IOptions<SchedulerOptions> optionsAccessor)
    //    {
    //        if (optionsAccessor != null)
    //            Config(optionsAccessor.Value);
    //    }

    //    public void Config(SchedulerOptions options)
    //    {
    //        _options = options ?? throw new ArgumentNullException(nameof(options));

    //        _timer = new Timer(Callback, null, 0, 1000);
    //    }

    //    public void Callback(object state)
    //    {
    //        Console.Write(".");

    //    }

    //    ~Scheduler()
    //    {
    //        if (_timer == null) return;

    //        _timer.Dispose();
    //        _timer = null;
    //    }
    //}
}
