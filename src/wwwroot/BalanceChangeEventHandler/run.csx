#r "Newtonsoft.Json"
#load "../utils/http.csx"
#load "../shared.csx"

using System;
using Newtonsoft.Json;

static IHttp http = new Http();

public class DetailedBusinessEvent
{
    public DateTime When { get; set; }
    public string AccountId { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public string SourceEventUri { get; set; }
}

public static void Run(
    string queueItem, 
    out DetailedBusinessEvent detailedBusinessEvent, 
    TraceWriter log)
{
    log.Info($"C# Queue trigger function processed: {queueItem}");

    var balanceChangeEvent = JsonConvert.DeserializeObject<BalanceChangedEventMessage>(queueItem);

    var response = http.Get<BalanceChangeEventResource>(balanceChangeEvent.EventUri);
    
    //var response = balanceChangeEvent.EventUri;
    //TODO:go fetch the event info



    //TODO:glue them together and return an event

    detailedBusinessEvent = new DetailedBusinessEvent
    {
        SourceEventUri = balanceChangeEvent.EventUri
    };
}