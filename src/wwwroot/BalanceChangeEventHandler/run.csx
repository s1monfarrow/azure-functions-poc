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

    var balanceChangeMessage = JsonConvert.DeserializeObject<BalanceChangedEventMessage>(queueItem);
    
    var eventResource = http.Get<BalanceChangeEventResource>(balanceChangeMessage.EventUri);

    var accountResource = http.Get<AccountResource>(eventResource.AccountUri);

    detailedBusinessEvent = new DetailedBusinessEvent
    {
        When = eventResource.Event.Timestamp,
        AccountId = eventResource.Event.AccountId,
        Amount = eventResource.Event.Amount,
        Balance = accountResource.Balance,
        SourceEventUri = balanceChangeMessage.EventUri
    };
}