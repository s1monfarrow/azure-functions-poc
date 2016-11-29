#load "utils/current.csx"
using System;

public static string accountUriTemplate = "http://account/{0}";

public class BalanceChangedEvent
{
    public BalanceChangedEvent()
    {
        Timestamp = Current.Offset;
        RowKey = Current.Guid.ToString();
        AccountId = "123";
        Amount = 3m;
        PartitionKey = "test";
    }

    public DateTimeOffset Timestamp { get; set; }
    public string AccountId { get; set; }
    public decimal Amount { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
}

public class BalanceChangedEventMessage
{
    public string EventUri { get; set; }
}


public class BalanceChangeEventResource
{
    public string AccountUri { get; set; }
    public BalanceChangedEvent Event { get; set; }
}

public class AccountResource
{
    public string Self { get; set; }
    public decimal Balance { get; set; }
}

public class DetailedBusinessEvent
{
    public DateTimeOffset Timestamp { get; set; }
    public string AccountId { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public string SourceEventUri { get; set; }
}

public class StagedDetailedBusinessEvent : DetailedBusinessEvent
{
    public StagedDetailedBusinessEvent(DetailedBusinessEvent eve)
    {
        Timestamp = eve.Timestamp;
        AccountId = eve.AccountId;
        Amount = eve.Amount;
        Balance = eve.Balance;
        SourceEventUri = eve.SourceEventUri;
    }

    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
}