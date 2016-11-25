using System;

public static string accountUriTemplate = "http://account/{0}";

public class BalanceChangedEvent
{
    public BalanceChangedEvent()
    {
        Timestamp = Current.Time;
        RowKey = Current.Guid.ToString();
        AccountId = "123";
        Amount = 3m;
        PartitionKey = "test";
    }

    public DateTime Timestamp { get; set; }
    public string AccountId { get; set; }
    public decimal Amount { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
}

public class BalanceChangedEventMessage
{
    public string EventUri { get; set; }
}
