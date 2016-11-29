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
    string queueItem,
    ICollector<StagedDetailedBusinessEvent> outTable, 
    TraceWriter log)
{
    log.Info($"C# Queue trigger function processed: {queueItem}");

    //This would use a dynamic binding to change table depending on
    //the content of the arriving message

    var businessEvent = JsonConvert.DeserializeObject<DetailedBusinessEvent>(queueItem);

    outTable.Add(new StagedDetailedBusinessEvent(businessEvent)
    {
        PartitionKey = "Test",
        RowKey = Current.Guid.ToString()
    });
}