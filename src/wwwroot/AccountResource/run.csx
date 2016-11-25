#r "Microsoft.WindowsAzure.Storage"
#load "../shared.csx"
#load "../utils/current.csx"

using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.Linq;

public static async Task<HttpResponseMessage> Run(
    HttpRequestMessage req,
    string accountid, 
    IQueryable<BalanceChangedEvent> inTable, 
    TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    //fetch the event
    var events = inTable.Where(e => e.AccountId == accountid);
    if (!events.Any())
        return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));

    var acc = new
    {
        @self = string.Format(accountUriTemplate, accountid),
        balance = events.Sum(e => e.Amount)
    };

    var res = req.CreateResponse(HttpStatusCode.OK, acc);
    return await Task.FromResult(res);
}
