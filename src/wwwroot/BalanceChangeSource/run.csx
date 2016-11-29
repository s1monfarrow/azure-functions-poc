#load "../utils/current.csx"
#load "../shared.csx"

using System;

public static string uriTemplate = "http://localhost/events/{0}";

public static void Run(
    TimerInfo myTimer, 
    ICollector<BalanceChangedEvent> outTable, 
    out BalanceChangedEventMessage outQueue, 
    TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    var eve = new BalanceChangedEvent();
    outTable.Add(eve);

    var resourceUrl = string.Format(uriTemplate, eve.RowKey);

    outQueue = new BalanceChangedEventMessage { EventUri = resourceUrl };
}