#r "Newtonsoft.Json"

using System;
using Newtonsoft.Json;

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

    dynamic order = JsonConvert.DeserializeObject(queueItem);

    //TODO:go fetch the account info

    //TODO:glue them together and return an event

    detailedBusinessEvent = new DetailedBusinessEvent
    {
        SourceEventUri = order.EventUri
    };
}