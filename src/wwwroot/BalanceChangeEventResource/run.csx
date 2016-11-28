#r "Microsoft.WindowsAzure.Storage"
#load "../shared.csx"
#load "../utils/current.csx"

using Microsoft.WindowsAzure.Storage.Table;
using System.Net;

public static string accountUriTemplate = "http://account/{0}";

public static async Task<HttpResponseMessage> Run(
    HttpRequestMessage req,
    string eventid, 
    IQueryable<BalanceChangedEvent> inTable, 
    TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    //fetch the event
    var eve = inTable.FirstOrDefault(e => e.RowKey == eventid);
    if (eve == null)
        return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
    
    var res = new BalanceChangeEventResource
    {
        AccountUri = string.Format(accountUriTemplate, eve.AccountId),
        Event = eve
    };

    var res = req.CreateResponse(HttpStatusCode.OK, res);
    return await Task.FromResult(res);
}
