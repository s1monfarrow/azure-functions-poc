#load "prelude.csx"
#load "../wwwroot/BalanceChangeSource/run.csx"

// Arrange
var storage = new List<BalanceChangedEvent>();
var table = MocCollector<BalanceChangedEvent>(storage);

Current.Guid = Guid.Empty;

BalanceChangedEventMessage message;
uriTemplate = "http://test/{0}";

// Action
Run(timer, table, out message, log);

// Assert
fa.Should(message.EventUri).Be("http://test/00000000-0000-0000-0000-000000000000");
fa.Should(storage.Count).Be(1);