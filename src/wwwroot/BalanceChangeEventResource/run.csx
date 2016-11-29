#r "Microsoft.WindowsAzure.Storage"
#load "../shared.csx"
#load "../utils/current.csx"

using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.Collections;

public static async Task<HttpResponseMessage> Run(
    HttpRequestMessage req,
    string eventid, 
    IQueryable<BalanceChangedEvent> inTable, 
    TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
        log.Info($"key={de.Key}  value={de.Value}");

    //fetch the event
    var eve = inTable
        .Where(e => e.PartitionKey == "test" && e.RowKey == eventid)
        .FirstOrDefault();
    if (eve == null)
        return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent(string.Format("No event with id = {0}", eventid)),
            ReasonPhrase = "Event Id Not Found"
        });
    
    var resource = new BalanceChangeEventResource
    {
        AccountUri = string.Format(accountUriTemplate, eve.AccountId),
        Event = eve
    };
    
    return await Task.FromResult(req.CreateResponse(HttpStatusCode.OK, resource));
}
