#load "prelude.csx"
#load "../wwwroot/BalanceChangeEventHandler/run.csx"

// Arrange
DetailedBusinessEvent message;

var eve = "{\"EventUri\":\"http://event/abc\"}";

http = new FakeHttp()
    .Add("http://event/abc", new BalanceChangeEventResource
    {
        AccountUri = "http://account/abc",
        Event = new BalanceChangedEvent()
    })
    .Add("http://account/abc", new AccountResource
    {
        Self = "AccountResource",
        Balance = 6m
    });

// Action
Run(eve, out message, log);

// Assert
fa.Should(message.SourceEventUri).Be("http://event/abc");
fa.Should(message.AccountId).Be("123");
fa.Should(message.Amount).Be(3m);
fa.Should(message.Balance).Be(6m);