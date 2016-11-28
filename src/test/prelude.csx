using System.Collections;
using Microsoft.Azure.WebJobs.Extensions;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Timers;

var context = Require<AzureFunctionsPackContext>();
var moq = Require<MoqPackContext>();
var fa = Require<FluentAssertionsPackContext>();

TraceWriter log = new TraceMonitor();

public ICollector<T> MocCollector<T>(List<T> storage)
{
    var mColl = moq.Mock<ICollector<T>>();
    mColl
        .Setup(f => f.Add(It.IsAny<T>()))
        .Callback((T e) => storage.Add(e));
    return mColl.Object;
}

var timer = new TimerInfo(new CronSchedule("0 */5 * * * *"));

//Might be worth extending this instead of mocking
public class FakeHttp<T> : IHttp
{
    T result;

    public FakeHttp(T obj)
    {
        result = obj;
    }

    public TRes Get<TRes>(string url) where TRes : class, new()
    {
        return result as TRes;
    }
}