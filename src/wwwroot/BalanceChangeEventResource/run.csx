#r "Microsoft.WindowsAzure.Storage"
#load "../shared.csx"
#load "../utils/current.csx"

using Microsoft.WindowsAzure.Storage.Table;
using System.Net;

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
    
    var resource = new BalanceChangeEventResource
    {
        AccountUri = string.Format(accountUriTemplate, eve.AccountId),
        Event = eve
    };
    
    return await Task.FromResult(req.CreateResponse(HttpStatusCode.OK, resource));
}
