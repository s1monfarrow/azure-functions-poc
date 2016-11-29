#r "Microsoft.WindowsAzure.Storage"
#r "Newtonsoft.Json"
#load "../utils/current.csx"
#load "../shared.csx"

using System;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.Collections;
using Newtonsoft.Json;

public static void Run(
    TimerInfo myTimer, 
    ICollector<BalanceChangedEvent> outTable, 
    out BalanceChangedEventMessage outQueue, 
    TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    var eve = new BalanceChangedEvent()
    {
        PartitionKey = "test",
        RowKey = Current.Guid.ToString()
    };

    outTable.Add(eve);

    var resourceUrl = string.Format(eventUriTemplate, eve.RowKey);

    outQueue = new BalanceChangedEventMessage { EventUri = resourceUrl };

    log.Info($"Event created at : {resourceUrl}");
}