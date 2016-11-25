#r "Microsoft.WindowsAzure.Storage"
#load "../utils/current.csx"

using System.Net;
using System.Collections;

public class FakeEvent
{
    public FakeEvent()
    {
        When = Current.Time;
        Id = Current.Guid.ToString();
    }

    public DateTime When { get; set; }
    public string Id { get; set; }
}

public class FakeFeed
{
    public FakeFeed()
    {
        Events = new List<FakeEvent> { new FakeEvent() };
    }

    public List<FakeEvent> Events { get; set; }
}


public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"Created a new event");
    var res = req.CreateResponse(HttpStatusCode.OK, new FakeFeed());
    return await Task.FromResult(res);
}
